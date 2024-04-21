using UnityEngine.InputSystem;

public class TutorialStageBuilder {
    private InputAction _mainAction = null;
    private bool _otherConditionsSatisfied = false;
    private TutorialNotification _tutorialNotification = null;

    public TutorialStageBuilder() {
    }

    public TutorialStageBuilder mainAction(InputAction inputAction) {
        _mainAction = inputAction;
        return this;
    }

    public TutorialStageBuilder conditionSatisfied(bool conditionSatisfied) {
        _otherConditionsSatisfied = conditionSatisfied;
        return this;
    }

    public TutorialStageBuilder tutorialNotification(TutorialNotification notification) {
        _tutorialNotification = notification;
        return this;
    }

    public TutorialStage build() {
        return new TutorialStage(_mainAction, _otherConditionsSatisfied, _tutorialNotification);
    }
}