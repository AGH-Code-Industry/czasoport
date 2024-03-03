using CustomInput;
using InventorySystem;
using LevelTimeChange.TimeChange;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    int stage = 0;
    List<TutorialStage> _stages = new List<TutorialStage>();
    List<TutorialNotification> _messages = new List<TutorialNotification>();
    float _timeToDisplayMessage = 1f;

    /// <summary>
    /// If you want to disable tutorial, simply deactivate the "tutorialManager" object in the scene "Game"
    /// </summary>
    void Start()
    {
        // Jak coś dodajecie to lepiej napiszcie do Mikołaja
        _stages.Add(new TutorialStage(CInput.InputActions.Movement.Navigation, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Interactions.Interaction, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Inventory.NextItem, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Inventory.ChooseItem, true));
        _stages.Add(new TutorialStage(CInput.InputActions.Interactions.Interaction, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Interactions.Interaction, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Teleport.TeleportBack, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Teleport.TeleportForward, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Inventory.Drop, false));
        _stages.Add(new TutorialStage(CInput.InputActions.Game.TogglePause, true));

        _messages.Add(new TutorialNotification("Cześć! Witaj w samouczku!"));
        _messages.Add(new TutorialNotification("Wciśnij", "WSAD", "aby się poruszać."));
        _messages.Add(new TutorialNotification("Wciśnij", "F", "aby podnieść przedmiot."));
        _messages.Add(new TutorialNotification("Wciśnij", "TAB", "aby wybrać kolejny slot."));
        _messages.Add(new TutorialNotification("Wciśnij liczby", "1-6", "aby wybrać slot w ekwipunku."));
        _messages.Add(new TutorialNotification("Wciśnij", "F", "aby wejść w interakcję z innym przedmiotem (spróbuj rozwalić kamień)."));
        _messages.Add(new TutorialNotification("Świetnie! Teraz poszukaj czasoportu."));
        _messages.Add(new TutorialNotification("Wciśnij", "Q", "aby teleportować się w przeszłość."));
        _messages.Add(new TutorialNotification("Wciśnij", "E", "aby teleportować się w przyszłość."));
        _messages.Add(new TutorialNotification("Wciśnij", "G", "aby wyrzucić aktualnie trzymany przedmiot."));
        _messages.Add(new TutorialNotification("Wciśnij", "ESCAPE", "aby zapauzować grę."));
        _messages.Add(new TutorialNotification("Brawo! Udało ci się przejść samouczek, teraz dopiero zaczyna się zabawa... Powodzenia!!!"));

        Interactions.Interactions.Instance.itemInteractionPerformed += ItemInteraction;
        Inventory.Instance.ItemInserted += ItemInserted;
        Inventory.Instance.ItemRemoved += ItemDropped;
        TimeChanger.Instance.TimeChangeUnlocked += TimelineUnlocked;
        TimeChanger.Instance.OnTimeChange += TimeChange;
        CInput.InputActions.Inventory.ChooseItem.performed += EnableInteractions;

        CInput.InputActions.Teleport.Disable();
        CInput.InputActions.Inventory.Disable();
        CInput.InputActions.Interactions.Disable();
        CInput.InputActions.Game.Disable();
        CInput.InputActions.Movement.Disable();

        StartCoroutine(waitForTutorialToBegin(0.01f));
    }

    private void EnableInteractions(InputAction.CallbackContext context) {
        CInput.InputActions.Interactions.Enable();
    }

    private void ItemInserted(object sender, EventArgs e) {
        if (stage == 1) _stages[stage].otherConditionsSatisfied = true;
        CInput.InputActions.Interactions.Disable();
    }

    private void ItemInteraction(object sender, EventArgs e) {
        if (stage == 4) _stages[stage].otherConditionsSatisfied = true;
    }

    private void TimelineUnlocked(object sender, EventArgs e) {
        if (stage == 5) _stages[stage].otherConditionsSatisfied = true;
    }

    private void TimeChange(object sender, TimeChanger.OnTimeChangeEventArgs e) {
        if (stage == 6 || stage == 7) {
            _stages[stage].otherConditionsSatisfied = true;
            OnNextTutorialStage(new InputAction.CallbackContext());
        }
    }

    private void ItemDropped(object sender, EventArgs e) {
        if (stage == 8) _stages[stage].otherConditionsSatisfied = true;
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
        } else {
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
