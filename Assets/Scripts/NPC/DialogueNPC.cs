using System.Collections;
using System.Collections.Generic;
using Dialogues;
using InteractableObjectSystem;
using UnityEngine;

namespace NPC {
    public class DialogueNPC : InteractableObject {
        [SerializeField] private TextAsset dialogue;
        public override void InteractionHand() {
            DialogueManager.I.StartDialogue(dialogue);
        }
    }
}
