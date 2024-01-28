using InteractableObjectSystem;
using Items;
using LevelTimeChange;
using LevelTimeChange.TimeChange;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Czasoport : InteractableObject
{
    public override void InteractionHand() {
        TimeChanger timeChanger = FindObjectOfType<TimeChanger>();
        timeChanger.EnableTimeChange();
        timeChanger.UnlockTimeline(TimeLine.Present);
        timeChanger.UnlockTimeline(TimeLine.Future);
        NotificationManager.Instance.RaiseNotification(definition.successfulHandInterNotification);
        FindObjectOfType<TimeChangeUI>().UpdateTimeUI();
        Destroy(this.gameObject);
    }

    public override bool InteractionItem(Item item) {
        InteractionHand();
        return false;
    }
}
