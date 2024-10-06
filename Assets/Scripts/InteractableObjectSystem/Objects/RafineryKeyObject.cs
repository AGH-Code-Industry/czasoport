using System.Collections;
using System.Collections.Generic;
using InteractableObjectSystem;
using Items;
using Notifications;
using UnityEngine;

public class RafineryKeyObject : InteractableObject {
    public override void InteractionHand() {
        NotificationManager.Instance.RaiseNotification(new Notification("I have to adjust this key if I want to grab it", 6f));
    }

    public override bool InteractionItem(Item item) {
        InteractionHand();
        return false;
    }
}