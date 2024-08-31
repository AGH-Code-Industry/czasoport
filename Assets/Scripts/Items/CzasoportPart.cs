using InteractableObjectSystem;
using Items;
using LevelTimeChange;
using LevelTimeChange.TimeChange;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CzasoportPart : InteractableObject {
    public override void InteractionHand() {
        TimeChanger timeChanger = FindObjectOfType<TimeChanger>();
        timeChanger.PickUpCzasoportPart(definition);
        Destroy(gameObject);
    }

    public override bool InteractionItem(Item item) {
        InteractionHand();
        return false;
    }
}