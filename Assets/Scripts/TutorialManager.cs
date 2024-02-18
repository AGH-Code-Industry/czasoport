using CustomInput;
using Notifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    int counter = 0;
    List<InputAction> _stages = new List<InputAction>();
    List<TutorialNotification> _messages = new List<TutorialNotification>();
    float _timeToDisplayMessage = 3f;

    /// <summary>
    /// If you want to disable tutorial, simply deactivate the "tutorialManager" object in the scene "Game"
    /// </summary>
    void Start()
    {
        _stages.Add(CInput.InputActions.Movement.Navigation);
        _stages.Add(CInput.InputActions.Interactions.Interaction);
        _stages.Add(CInput.InputActions.Teleport.TeleportForward);
        _stages.Add(CInput.InputActions.Teleport.TeleportBack);
        _stages.Add(CInput.InputActions.Game.TogglePause);
        _stages.Add(CInput.InputActions.Inventory.ChooseItem);


        //_messages.Add(new TutorialNotification("Cześć! Witaj w samouczku!", "", ""));
        _messages.Add(new TutorialNotification("Wciśnij", "WSAD", "aby się poruszać."));
        _messages.Add(new TutorialNotification("Wciśnij", "F", "aby podnieść przedmiot lub wejść w interakcję z innym przedmiotem"));
        _messages.Add(new TutorialNotification("Wciśnij", "E", "aby teleportować się w przyszłość"));
        _messages.Add(new TutorialNotification("Wciśnij", "Q", "aby teleportować się w przeszłość"));
        _messages.Add(new TutorialNotification("Wciśnij", "ESCAPE", "aby zapauzować grę"));
        _messages.Add(new TutorialNotification("Wciśnij liczby", "1-5", "aby wybrać slot w ekwipunku"));
        _messages.Add(new TutorialNotification("", "", "Brawo! Udało ci się przejść samouczek, teraz dopiero zaczyna się zabawa... Powodzenia!!!"));

        CInput.InputActions.Teleport.Disable();
        CInput.InputActions.Inventory.Disable();
        CInput.InputActions.Interactions.Disable();
        CInput.InputActions.Game.Disable();
        CInput.InputActions.Movement.Disable();

        StartCoroutine(waitForTutorialToBegin(0.01f));
    }

    IEnumerator waitForTutorialToBegin(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        CInput.InputActions.Movement.Enable();
        //NotificationManager.Instance.RaiseNotification(new Notification(_messages[0], _timeToDisplayMessage));
        //NotificationManager.Instance.RaiseTutorialNotification(_messages[0]);
        //yield return new WaitForSeconds(_timeToDisplayMessage);
        CInput.InputActions.Movement.Navigation.performed += OnNextTutorialStage;
        //NotificationManager.Instance.RaiseNotification(new Notification(_messages[1], _timeToDisplayMessage));
        NotificationManager.Instance.RaiseTutorialNotification(_messages[0]);
    }

    /// <summary>
    /// This function enables next action that now player can perform. That way the player learns how to play step by step.
    /// The order of the stages of tutorial is set by the list "_stages".
    /// </summary>
    private void OnNextTutorialStage(InputAction.CallbackContext context) {
        _stages[counter].performed -= OnNextTutorialStage;
        if (counter + 1 < _stages.Count) {
            NotificationManager.Instance.RaiseTutorialNotification(_messages[counter+1]);
            _stages[counter + 1].Enable();
            _stages[counter + 1].performed += OnNextTutorialStage;
            counter++;
        } else {
            tutorialFinished();
        }
    }

    /// <summary>
    /// Enable all functionalities as the tutorial is finished.
    /// </summary>
    private void tutorialFinished() {
        NotificationManager.Instance.RaiseTutorialNotification(_messages[_messages.Count - 1]);
        CInput.InputActions.Teleport.Enable();
        CInput.InputActions.Inventory.Enable();
        CInput.InputActions.Interactions.Enable();
        CInput.InputActions.Game.Enable();
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial() {
        yield return new WaitForSeconds(3f);
        NotificationManager.Instance.EndTutorial();
    }
}
