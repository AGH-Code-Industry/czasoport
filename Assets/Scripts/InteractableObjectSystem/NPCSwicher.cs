using Dialogues;
using Interactions;
using Items;
using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSwicher : MonoBehaviour {
    [SerializeField] private GameObject npcToEnable;
    [SerializeField] private GameObject npcToDisable;
    [SerializeField] private ItemSO exchangingItem;
    [SerializeField] private Material _nonRedMaterial;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem == null || exchangingItem != this.exchangingItem) {
            return;
        }
        SwitchNPCs();
    }

    public void SwitchNPCs() {
        npcToEnable.SetActive(true);

        //changing dialogue, animator and sprite
        if (npcToEnable.GetComponent<DialogueNPC>() != null && npcToDisable.GetComponent<DialogueNPC>() != null) {
            npcToDisable.GetComponent<DialogueNPC>().SetDialogue(npcToEnable.GetComponent<DialogueNPC>().GetDialogue());
        }
        else {
            Destroy(npcToDisable.GetComponent<DialogueNPC>());
            Destroy(npcToDisable.GetComponent<HighlightInteraction>());
        }
        npcToDisable.GetComponent<Animator>().runtimeAnimatorController = npcToEnable.GetComponent<Animator>().runtimeAnimatorController;
        npcToDisable.GetComponent<SpriteRenderer>().sprite = npcToEnable.GetComponent<SpriteRenderer>().sprite;

        //making the NPC not white
        SpriteRenderer sprite = npcToDisable.GetComponent<SpriteRenderer>();
        sprite.material = _nonRedMaterial;
        sprite.material.SetTexture("_texture", sprite.sprite.texture);
        npcToEnable.SetActive(false);
    }
}