using Dialogues;
using Items;
using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurmistrzMover : MonoBehaviour
{
    [SerializeField] private ItemSO exchangingItem;
    [SerializeField] private List<PathWalking> _NPCPathWalkings = new();

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem == this.exchangingItem) {
            foreach (var pathWalking in _NPCPathWalkings) {
                pathWalking.StartWalk();
            }
        }
    }

}
