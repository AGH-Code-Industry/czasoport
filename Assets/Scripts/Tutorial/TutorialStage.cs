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

    public TutorialStage(InputAction action, bool b, TutorialNotification _tutorialNotification) {
        _mainAction = action;
        _otherConditionsSatisfied = b;
        this._tutorialNotification = _tutorialNotification;
        if (action != null) _mainAction.performed += performAction;
    }

    private void performAction(InputAction.CallbackContext context) {
        _actionPerformed = true;
    }

    public void satisfyConditions() {
        _otherConditionsSatisfied = true;
    }

    public bool isConditionSatisfied() {
        return _otherConditionsSatisfied;
    }

    public TutorialNotification getTutorialNotification() {
        return _tutorialNotification;
    }

    public InputAction getMainAction() {
        return _mainAction;
    }

    public bool isActionPerformed() {
        return _actionPerformed;
    }
}