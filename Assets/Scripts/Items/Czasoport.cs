using InteractableObjectSystem;
using Items;
using LevelTimeChange;
using LevelTimeChange.TimeChange;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using UI;
using UnityEngine;

public class Czasoport : PersistentInteractableObject {



    public override void InteractionHand() {
        var timeChanger = FindObjectOfType<TimeChanger>();
        timeChanger.PickUpCzasoport();
        NotificationManager.Instance.RaiseNotification(definition.successfulHandInterNotification);
        Destroy(gameObject);
    }

    public override bool InteractionItem(Item item) {
        InteractionHand();
        return false;
    }

    public override void LoadPersistentData(GameData gameData) {
        if (gameData.playerGameData.hasCzasoport)
            Destroy(gameObject);
    }

    public override void SavePersistentData(ref GameData gameData) {
    }
}