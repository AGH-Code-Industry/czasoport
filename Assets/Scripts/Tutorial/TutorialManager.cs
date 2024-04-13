using CustomInput;
using InventorySystem;
using LevelTimeChange.TimeChange;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InventorySystem.EventArguments;

public class TutorialManager : MonoBehaviour {
    int stage = 1;
    List<TutorialStage> _stages = new List<TutorialStage>();
    List<TutorialNotification> _messages = new List<TutorialNotification>();
    float _timeToDisplayMessage = 1f;

    /// <summary>
    /// If you want to disable tutorial, simply deactivate the "tutorialManager" object in the scene "Game"
    /// </summary>
    void Start() {
        Timer.instance.StartTimer();
        // Jak coś dodajecie to lepiej napiszcie do Mikołaja

        // Create tutorial stages
        TutorialStage initialTutorialStage = new TutorialStageBuilder()
            .tutorialNotification(new TutorialNotification("Hello! Welcome to the tutorial!"))
            .build();

        TutorialStage movementTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Movement.Navigation)
            .conditionSatisfied(true)
            .tutorialNotification(new TutorialNotification("Press", "WSAD", "to move."))
            .build();

        TutorialStage pauseTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Game.TogglePause)
            .conditionSatisfied(true)
            .tutorialNotification(new TutorialNotification("Press", "ESCAPE", "to pause the game."))
            .build();

        TutorialStage pickUpItemTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Interactions.Interaction)
            .conditionSatisfied(false)
            .tutorialNotification(new TutorialNotification("Press", "F", "to pick up an item."))
            .build();

        TutorialStage chooseNextItemTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Inventory.NextItem)
            .conditionSatisfied(true)
            .tutorialNotification(new TutorialNotification("Press", "TAB", "to select the next slot in you inventory."))
            .build();

        TutorialStage chooseSlotItemTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Inventory.ChooseItem)
            .conditionSatisfied(true)
            .tutorialNotification(new TutorialNotification("Press numbers", "1-6", "to select specific slot."))
            .build();

        TutorialStage dropItemTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Inventory.Drop)
            .conditionSatisfied(false)
            .tutorialNotification(new TutorialNotification("Press", "G", "drop an item."))
            .build();

        TutorialStage breakeRockTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Interactions.Interaction)
            .conditionSatisfied(false)
            .tutorialNotification(new TutorialNotification("Press", "F", "to interack with another item (try breaking the rock)."))
            .build();

        TutorialStage pickUpTimeportTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Interactions.Interaction)
            .conditionSatisfied(false)
            .tutorialNotification(new TutorialNotification("Good job! Now try to find timeport."))
            .build();

        TutorialStage teleportBackTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Teleport.TeleportBack)
            .conditionSatisfied(false)
            .tutorialNotification(new TutorialNotification("Press", "Q", "to teleport back in time."))
            .build();

        TutorialStage teleportForwardTutorialStage = new TutorialStageBuilder()
            .mainAction(CInput.InputActions.Teleport.TeleportForward)
            .conditionSatisfied(false)
            .tutorialNotification(new TutorialNotification("Press", "E", "to teleport to the future."))
            .build();

        TutorialStage endingTutorialStage = new TutorialStageBuilder()
            .tutorialNotification(new TutorialNotification("Congratulations! You managed to pass the tutorial, now the fun begin... Good luck!!!"))
            .build();

        // Order in which messages are displayed on the screen.
        _stages = new List<TutorialStage> {
            initialTutorialStage,
            movementTutorialStage,
            pauseTutorialStage,
            pickUpItemTutorialStage,
            chooseNextItemTutorialStage,
            chooseSlotItemTutorialStage,
            dropItemTutorialStage,
            breakeRockTutorialStage,
            pickUpTimeportTutorialStage,
            teleportBackTutorialStage,
            teleportForwardTutorialStage,
            endingTutorialStage
        };

        /*{
            {
                notification: "notification",
                otherConditions: false,
                additionalBehaviour: IP
            }, { }...
        }*/

        // Here define how additional conditions to your tutorial stage are going to be satisfied
        Interactions.Interactions.Instance.itemInteractionPerformed += (object sender, EventArgs e) => breakeRockTutorialStage.satisfyConditions();

        Inventory.Instance.ItemInserted += (object sender, ItemInsertedEventArgs e) => pickUpItemTutorialStage.satisfyConditions();

        Inventory.Instance.ItemRemoved += (object sender, ItemRemovedEventArgs e) => dropItemTutorialStage.satisfyConditions();

        TimeChanger.Instance.TimeChangeUnlocked += (object sender, EventArgs e) => pickUpTimeportTutorialStage.satisfyConditions();

        TimeChanger.Instance.OnTimeChange += (object sender, TimeChanger.OnTimeChangeEventArgs e) => {
            if ((int)e.time - (int)e.previousTime == -1) teleportBackTutorialStage.satisfyConditions();
        };
        TimeChanger.Instance.OnTimeChange += (object sender, TimeChanger.OnTimeChangeEventArgs e) => {
            if ((int)e.time - (int)e.previousTime == 1) teleportForwardTutorialStage.satisfyConditions();
        };

        StartCoroutine(waitForTutorialToBegin(0.01f));
    }

    IEnumerator waitForTutorialToBegin(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        NotificationManager.Instance.StartTutorial();
        CInput.InputActions.Movement.Enable();
        NotificationManager.Instance.RaiseTutorialNotification(_stages[0].getTutorialNotification());
        yield return new WaitForSeconds(_timeToDisplayMessage);
        _stages[1].getMainAction().performed += OnNextTutorialStage;
        NotificationManager.Instance.RaiseTutorialNotification(_stages[1].getTutorialNotification());
    }

    /// <summary>
    /// This function enables next action that now player can perform. That way the player learns how to play step by step.
    /// The order of the stages of tutorial is set by the list "_stages".
    /// </summary>
    private void OnNextTutorialStage(InputAction.CallbackContext context) {
        if (!_stages[stage].isConditionSatisfied()) return;
        _stages[stage].getMainAction().performed -= OnNextTutorialStage;
        while (stage + 1 < _stages.Count && _stages[stage].isActionPerformed() && _stages[stage].isConditionSatisfied()) { stage++; }
        if (stage + 1 < _stages.Count) {
            NotificationManager.Instance.RaiseTutorialNotification(_stages[stage].getTutorialNotification());
            _stages[stage].getMainAction().Enable();
            _stages[stage].getMainAction().performed += OnNextTutorialStage;
        }
        else {
            TutorialFinished();
        }
    }

    /// <summary>
    /// Enable all functionalities as the tutorial is finished.
    /// </summary>
    private void TutorialFinished() {
        NotificationManager.Instance.RaiseTutorialNotification(_stages[_stages.Count - 1].getTutorialNotification());
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial() {
        yield return new WaitForSeconds(3f);
        NotificationManager.Instance.EndTutorial();
    }
}