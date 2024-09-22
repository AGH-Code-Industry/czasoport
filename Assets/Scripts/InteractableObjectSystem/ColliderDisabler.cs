using Dialogues;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisabler : MonoBehaviour
{
    [SerializeField] private Collider2D colliderToDisable;
    [SerializeField] private ItemSO exchangingItem;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem == this.exchangingItem) {
            colliderToDisable.enabled = false;
        }
    }
}
