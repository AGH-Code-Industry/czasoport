using System.Collections;
using System.Collections.Generic;
using Interactions;
using Items;
using NPC;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class NoFireSign : InteractableObject {
        [SerializeField] private ItemSO _interactedWith;
        [SerializeField] private List<PathWalking> _NPCPathWalkings = new();

        public override bool InteractionItem(Item item) {
            if (item.ItemSO == _interactedWith) {
                RaiseAlarm();
                return true;
            }

            return false;
        }

        private void RaiseAlarm() {
            foreach (var pathWalking in _NPCPathWalkings) {
                pathWalking.StartWalk();
            }
        }
    }
}