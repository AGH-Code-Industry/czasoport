using System;
using System.Collections;
using System.Collections.Generic;
using Dialogues;
using InteractableObjectSystem;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace NPC {
    public class DialogueNPC : InteractableObject {
        [SerializeField] private TextAsset dialogue;
        [SerializeField] private UnityEvent afterDialog = null;

        public override void InteractionHand() {
            DialogueManager.I.StartDialogue(dialogue, afterDialog.Invoke);
        }

        public override bool InteractionItem(Item item) {
            InteractionHand();
            return false;
        }
    }
}