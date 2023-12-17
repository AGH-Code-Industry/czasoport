using InteractableObjectSystem;
using LevelTimeChange;
using LevelTimeChange.TimeChange;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CzasoportPart : InteractableObject {
    public override void InteractionHand() {
        TimeChanger timeChanger = FindObjectOfType<TimeChanger>();
        if (!timeChanger.IsTimeUnlocked(TimeLine.Present)) {
            NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
            return;
        }
        timeChanger.UnlockTimeline(TimeLine.Past);
        NotificationManager.Instance.RaiseNotification(definition.successfulHandInterNotification);
        FindObjectOfType<TimeChangeUI>().UpdateTimeUI();
        Destroy(this.gameObject);
    }
}
