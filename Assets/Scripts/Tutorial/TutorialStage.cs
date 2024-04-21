using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Fluent builder pattern:
/// https://medium.com/xebia-engineering/fluent-builder-pattern-with-a-real-world-example-7b61be375a40
/// </summary>
/// 
public class TutorialStage {
    private InputAction _mainAction;
    private bool _otherConditionsSatisfied;
    private TutorialNotification _tutorialNotification;
    private bool _actionPerformed = false;

    public TutorialStage(InputAction action, bool otherConditionsSatisfied, TutorialNotification tutorialNotification) {
        _mainAction = action;
        _otherConditionsSatisfied = otherConditionsSatisfied;
        this._tutorialNotification = tutorialNotification;
        if (action != null) _mainAction.performed += PerformAction;
    }

    private void PerformAction(InputAction.CallbackContext context) {
        _actionPerformed = true;
    }

    public void SatisfyConditions() {
        _otherConditionsSatisfied = true;
    }

    public bool IsConditionSatisfied() {
        return _otherConditionsSatisfied;
    }

    public TutorialNotification GetTutorialNotification() {
        return _tutorialNotification;
    }

    public InputAction GetMainAction() {
        return _mainAction;
    }

    public bool IsActionPerformed() {
        return _actionPerformed;
    }
}