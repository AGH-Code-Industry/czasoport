using CustomInput;
using InventorySystem;
using LevelTimeChange.TimeChange;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InventorySystem.EventArguments;
using LevelTimeChange.LevelsLoader;
using Ink.Runtime;
using System.IO;
using DataPersistence;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour, IDataPersistence {
    public bool SceneObject { get; } = false;

    int stage = 1;
    List<TutorialStage> _stages = new List<TutorialStage>();
    List<TutorialNotification> _messages = new List<TutorialNotification>();
    float _timeToDisplayMessage = 1f;
    Dictionary<string, InputAction> _actionsDictionary = new Dictionary<string, InputAction>();
    private bool _tutorialFinished = false;

    /// <summary>
    /// If you want to disable tutorial, simply deactivate the "tutorialManager" object in the scene "Game"
    /// </summary>
    void Start() {
        Timer.instance.StartTimer();
        // Jak coś dodajecie to lepiej napiszcie do Mikołaja

        _actionsDictionary.Add("Navigation", CInput.InputActions.Movement.Navigation);
        _actionsDictionary.Add("TogglePause", CInput.InputActions.Game.TogglePause);
        _actionsDictionary.Add("Interaction", CInput.InputActions.Interactions.Interaction);
        _actionsDictionary.Add("NextItem", CInput.InputActions.Inventory.NextItem);
        _actionsDictionary.Add("ChooseItem", CInput.InputActions.Inventory.ChooseItem);
        _actionsDictionary.Add("Drop", CInput.InputActions.Inventory.Drop);
        _actionsDictionary.Add("TeleportBack", CInput.InputActions.Teleport.TeleportBack);
        _actionsDictionary.Add("TeleportForward", CInput.InputActions.Teleport.TeleportForward);
        _actionsDictionary.Add("", null);

        // Parse data from TutorialStages.json file
        TextAsset json = Resources.Load<TextAsset>("Tutorial/TutorialStages");
        _stages = ParseJsonArray(json.text);

        LevelsManager.Instance.OnLevelChange += FinishTutorialOnLevelChange;
    }


    private List<TutorialStage> ParseJsonArray(string jsonString) {
        List<TutorialStage> tutorialStages = new List<TutorialStage>();
        var jsonArray = jsonString.Trim().TrimStart('[').TrimEnd(']').Split('}');

        bool flag = true;
        foreach (var jsonStr in jsonArray) {
            if (string.IsNullOrWhiteSpace(jsonStr))
                continue;

            string trimmedJson;
            if (!flag) {
                trimmedJson = jsonStr.Substring(1).Trim() + "}";
            }
            else {
                trimmedJson = jsonStr.Trim() + "}";
            }
            flag = false;

            TutorialStageParser tutorialStageParser = JsonUtility.FromJson<TutorialStageParser>(trimmedJson);

            TutorialStage tutorialStage = new TutorialStageBuilder()
                .conditionSatisfied(tutorialStageParser.conditionSatisfied != null ? bool.Parse(tutorialStageParser.conditionSatisfied) : false)
                .mainAction(_actionsDictionary[tutorialStageParser.action])
                .tutorialNotification(new TutorialNotification(tutorialStageParser.tutorialNotification))
                .build();

            switch (tutorialStageParser.specialCondition) {
                case "ItemInserted":
                    Inventory.Instance.ItemInserted += (object sender, ItemInsertedEventArgs e) => tutorialStage.SatisfyConditions();
                    break;
                case "ItemRemoved":
                    Inventory.Instance.ItemRemoved += (object sender, ItemRemovedEventArgs e) => tutorialStage.SatisfyConditions();
                    break;
                case "itemInteractionPerformed":
                    Interactions.Interactions.Instance.itemInteractionPerformed += (object sender, EventArgs e) => tutorialStage.SatisfyConditions();
                    break;
                case "OnTimeChangeForward":
                    TimeChanger.Instance.OnTimeChange += (object sender, TimeChanger.OnTimeChangeEventArgs e) => {
                        if ((int)e.time - (int)e.previousTime == 1) tutorialStage.SatisfyConditions();
                    };
                    break;
                case "OnTimeChangeBackward":
                    TimeChanger.Instance.OnTimeChange += (object sender, TimeChanger.OnTimeChangeEventArgs e) => {
                        if ((int)e.time - (int)e.previousTime == -1) tutorialStage.SatisfyConditions();
                    };
                    break;
                case "TimeChangeUnlocked":
                    TimeChanger.Instance.TimeChangeUnlocked += (object sender, EventArgs e) => tutorialStage.SatisfyConditions();
                    break;
            }

            tutorialStages.Add(tutorialStage);
        }

        return tutorialStages;
    }

    private void FinishTutorialOnLevelChange(object sender, EventArgs e) {
        TutorialFinished();
    }

    IEnumerator waitForTutorialToBegin(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        NotificationManager.Instance.StartTutorial();
        CInput.InputActions.Movement.Enable();
        NotificationManager.Instance.RaiseTutorialNotification(_stages[0].GetTutorialNotification());
        yield return new WaitForSeconds(_timeToDisplayMessage);
        _stages[1].GetMainAction().performed += OnNextTutorialStage;
        NotificationManager.Instance.RaiseTutorialNotification(_stages[1].GetTutorialNotification());
    }

    /// <summary>
    /// This function enables next action that now player can perform. That way the player learns how to play step by step.
    /// The order of the stages of tutorial is set by the list "_stages".
    /// </summary>
    private void OnNextTutorialStage(InputAction.CallbackContext context) {
        if (!_stages[stage].IsConditionSatisfied()) return;
        _stages[stage].GetMainAction().performed -= OnNextTutorialStage;
        while (stage + 1 < _stages.Count && _stages[stage].IsActionPerformed() && _stages[stage].IsConditionSatisfied()) { stage++; }
        if (stage + 1 < _stages.Count) {
            NotificationManager.Instance.RaiseTutorialNotification(_stages[stage].GetTutorialNotification());
            _stages[stage].GetMainAction().Enable();
            _stages[stage].GetMainAction().performed += OnNextTutorialStage;
        }
        else {
            TutorialFinished();
        }
    }

    /// <summary>
    /// Enable all functionalities as the tutorial is finished.
    /// </summary>
    private void TutorialFinished() {
        if (_tutorialFinished) return;
        _tutorialFinished = true;
        NotificationManager.Instance.RaiseTutorialNotification(_stages[_stages.Count - 1].GetTutorialNotification());
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial() {
        yield return new WaitForSeconds(3f);
        NotificationManager.Instance.EndTutorial();
    }

    public void LoadPersistentData(GameData gameData) {
        _tutorialFinished = gameData.tutorialFinished;
        if (!_tutorialFinished)
            StartCoroutine(waitForTutorialToBegin(0.01f));
    }

    public void SavePersistentData(ref GameData gameData) {
        gameData.tutorialFinished = _tutorialFinished;
    }
}