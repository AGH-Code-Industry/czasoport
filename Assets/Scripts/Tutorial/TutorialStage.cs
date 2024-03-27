using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialStage {
    public InputAction mainAction;
    public bool otherConditionsSatisfied;

    public TutorialStage(InputAction action, bool b) {
        mainAction = action;
        otherConditionsSatisfied = b;
    }
}