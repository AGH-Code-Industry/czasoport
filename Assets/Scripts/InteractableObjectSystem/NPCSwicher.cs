using Dialogues;
using Items;
using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class NPCSwicher : MonoBehaviour {
    [SerializeField] private GameObject npcToEnable;
    [SerializeField] private GameObject npcToDisable;
    [SerializeField] private ItemSO exchangingItem;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem == this.exchangingItem) {
            npcToDisable.SetActive(false);
            npcToEnable.SetActive(true);
        }
    }
}