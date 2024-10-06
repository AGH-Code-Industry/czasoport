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
            npcToEnable.SetActive(true);

            //changing dialogue, animator and sprite
            if (npcToEnable.GetComponent<DialogueNPC>() != null && npcToDisable.GetComponent<DialogueNPC>() != null) {
                npcToDisable.GetComponent<DialogueNPC>().SetDialogue(npcToEnable.GetComponent<DialogueNPC>().GetDialogue());
            }
            npcToDisable.GetComponent<Animator>().runtimeAnimatorController = npcToEnable.GetComponent<Animator>().runtimeAnimatorController;
            npcToDisable.GetComponent<SpriteRenderer>().sprite = npcToEnable.GetComponent<SpriteRenderer>().sprite;

            //making the NPC not white
            SpriteRenderer sprite = npcToDisable.GetComponent<SpriteRenderer>();
            sprite.material.SetTexture("_texture", sprite.sprite.texture);
            npcToEnable.SetActive(false);
        }
    }
}