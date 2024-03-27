using System.Collections;
using System.Collections.Generic;
using Dialogues;
using InteractableObjectSystem;
using Items;
using UnityEngine;

namespace NPC {
    public class DialogueNPC : InteractableObject {
        [SerializeField] private TextAsset dialogue;
        public override void InteractionHand() {
            DialogueManager.I.StartDialogue(dialogue);
        }

        public override bool InteractionItem(Item item) {
            InteractionHand();
            return false;
        }
    }
}