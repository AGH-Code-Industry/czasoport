using CustomInput;
using InventorySystem;
using LevelTimeChange.TimeChange;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour {
    int stage = 0;
    List<TutorialStage> _stages = new List<TutorialStage>();
    List<TutorialNotification> _messages = new List<TutorialNotification>();
    float _timeToDisplayMessage = 1f;

    /// <summary>
    /// If you want to disable tutorial, simply deactivate the "tutorialManager" object in the scene "Game"
    /// </summary>
    void Start() {
        Timer.instance.StartTimer();
        // Jak coś dodajecie to lepiej napiszcie do Mikołaja
        _stages.Add(new TutorialStage(CInput.InputActions.Movement.Navigation, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Game.TogglePause, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Interactions.Interaction, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Inventory.NextItem, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Inventory.ChooseItem, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Inventory.Drop, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Interactions.Interaction, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Interactions.Interaction, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Teleport.TeleportBack, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Teleport.TeleportForward, false));

        _messages.Add(new TutorialNotification("Hello! Welcome to the tutorial!"));
        _messages.Add(new TutorialNotification("Press", "WSAD", "to move."));
        _messages.Add(new TutorialNotification("Press", "ESCAPE", "to pause the game."));
        _messages.Add(new TutorialNotification("Press", "F", "to pick up an item."));
        _messages.Add(new TutorialNotification("Press", "TAB", "to select the next slot in you inventory."));
        _messages.Add(new TutorialNotification("Press numbers", "1-6", "to select specific slot."));
        _messages.Add(new TutorialNotification("Press", "G", "drop an item."));
        _messages.Add(new TutorialNotification("Press", "F", "to interack with another item (try breaking the rock)."));
        _messages.Add(new TutorialNotification("Good job! Now try to find timeport."));
        _messages.Add(new TutorialNotification("Press", "Q", "to teleport back in time."));
        _messages.Add(new TutorialNotification("Press", "E", "to teleport to the future."));

        _messages.Add(new TutorialNotification("Congratulations! You managed to pass the tutorial, now the fun begin... Good luck!!!"));

        Interactions.Interactions.Instance.itemInteractionPerformed += ItemInteraction;
        Inventory.Instance.ItemInserted += ItemInserted;
        Inventory.Instance.ItemRemoved += ItemDropped;
        Inventory.Instance.ItemRemoved += EnableInteractions;
        TimeChanger.Instance.TimeChangeUnlocked += TimelineUnlocked;
        TimeChanger.Instance.OnTimeChange += TimeChange;

        CInput.InputActions.Teleport.Disable();
        CInput.InputActions.Inventory.Disable();
        CInput.InputActions.Interactions.Disable();
        CInput.InputActions.Game.Disable();
        CInput.InputActions.Movement.Disable();

        StartCoroutine(waitForTutorialToBegin(0.01f));
    }

    private void EnableInteractions(object sender, EventArgs e) {
        CInput.InputActions.Interactions.Enable();
        Inventory.Instance.ItemRemoved -= EnableInteractions;
    }

    private void ItemInserted(object sender, EventArgs e) {
        if (stage == 2) {
            _stages[stage].otherConditionsSatisfied = true;
            CInput.InputActions.Interactions.Disable();
        }
    }

    private void ItemDropped(object sender, EventArgs e) {
        if (stage == 5) _stages[stage].otherConditionsSatisfied = true;
    }

    private void ItemInteraction(object sender, EventArgs e) {
        if (stage == 6) _stages[stage].otherConditionsSatisfied = true;
    }

    private void TimelineUnlocked(object sender, EventArgs e) {
        if (stage == 7) _stages[stage].otherConditionsSatisfied = true;
    }

    private void TimeChange(object sender, TimeChanger.OnTimeChangeEventArgs e) {
        if (stage == 8 || stage == 9) {
            _stages[stage].otherConditionsSatisfied = true;
            OnNextTutorialStage(new InputAction.CallbackContext());
        }
    }


    IEnumerator waitForTutorialToBegin(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        NotificationManager.Instance.StartTutorial();
        CInput.InputActions.Movement.Enable();
        NotificationManager.Instance.RaiseTutorialNotification(_messages[0]);
        yield return new WaitForSeconds(_timeToDisplayMessage);
        CInput.InputActions.Movement.Navigation.performed += OnNextTutorialStage;
        NotificationManager.Instance.RaiseTutorialNotification(_messages[1]);
    }

    /// <summary>
    /// This function enables next action that now player can perform. That way the player learns how to play step by step.
    /// The order of the stages of tutorial is set by the list "_stages".
    /// </summary>
    private void OnNextTutorialStage(InputAction.CallbackContext context) {
        if (!_stages[stage].otherConditionsSatisfied) return;
        _stages[stage].mainAction.performed -= OnNextTutorialStage;
        if (stage + 1 < _stages.Count) {
            NotificationManager.Instance.RaiseTutorialNotification(_messages[stage + 2]);
            _stages[stage + 1].mainAction.Enable();
            _stages[stage + 1].mainAction.performed += OnNextTutorialStage;
            stage++;
        }
        else {
            TutorialFinished();
        }
    }

    /// <summary>
    /// Enable all functionalities as the tutorial is finished.
    /// </summary>
    private void TutorialFinished() {
        NotificationManager.Instance.RaiseTutorialNotification(_messages[_messages.Count - 1]);
        CInput.InputActions.Teleport.Enable();
        CInput.InputActions.Inventory.Enable();
        CInput.InputActions.Interactions.Enable();
        CInput.InputActions.Game.Enable();
        StartCoroutine(EndTutorial());

        Interactions.Interactions.Instance.itemInteractionPerformed -= ItemInteraction;
        Inventory.Instance.ItemInserted -= ItemInserted;
        TimeChanger.Instance.TimeChangeUnlocked -= TimelineUnlocked;
        TimeChanger.Instance.OnTimeChange -= TimeChange;
    }

    IEnumerator EndTutorial() {
        yield return new WaitForSeconds(3f);
        NotificationManager.Instance.EndTutorial();
    }
}