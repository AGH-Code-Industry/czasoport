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
    List<String> _messages = new List<string>();
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
        

        _messages.Add("Cześć! Witaj w samouczku! Wiem, że chcesz już zacząć... Niech tak będzie.");
        _messages.Add("Użyj WSAD, aby się poruszać.");
        _messages.Add("Chcesz coś podnieść? Użyj F.");
        _messages.Add("Teraz możesz się teleportować!! Teleportuj się w przyszłość za pomocą klawsza E.");
        _messages.Add("No tak tak, w przeszłość też możesz... Wciśnij Q.");
        _messages.Add("Potrzebujesz przerwy, żeby pomyśleć? Wciśnij ESCAPE.");
        _messages.Add("Chcesz wybrać konkretny item w swoim ekwipunku? Użyj do tego liczb 1-5.");
        _messages.Add("Brawo! Udało ci się przejść samouczek, teraz dopiero zaczyna się zabawa... Powodzenia!!!");

        CInput.InputActions.Teleport.Disable();
        CInput.InputActions.Inventory.Disable();
        CInput.InputActions.Interactions.Disable();
        CInput.InputActions.Game.Disable();
        CInput.InputActions.Movement.Disable();

        StartCoroutine(waitForTutorialToBegin(3f));
    }

    IEnumerator waitForTutorialToBegin(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        CInput.InputActions.Movement.Enable();
        NotificationManager.Instance.RaiseNotification(new Notification(_messages[0], _timeToDisplayMessage));
        yield return new WaitForSeconds(_timeToDisplayMessage);
        CInput.InputActions.Movement.Navigation.performed += OnNextTutorialStage;
        NotificationManager.Instance.RaiseNotification(new Notification(_messages[1], _timeToDisplayMessage));
    }

    /// <summary>
    /// This function enables next action that now player can perform. That way the player learns how to play step by step.
    /// The order of the stages of tutorial is set by the list "_stages".
    /// </summary>
    private void OnNextTutorialStage(InputAction.CallbackContext context) {
        _stages[counter].performed -= OnNextTutorialStage;
        if (counter + 1 < _stages.Count) {
            NotificationManager.Instance.RaiseNotification(new Notification(_messages[counter+2], _timeToDisplayMessage));
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
        NotificationManager.Instance.RaiseNotification(new Notification(_messages[_messages.Count - 1], _timeToDisplayMessage));
        CInput.InputActions.Teleport.Enable();
        CInput.InputActions.Inventory.Enable();
        CInput.InputActions.Interactions.Enable();
        CInput.InputActions.Game.Enable();
    }
}
