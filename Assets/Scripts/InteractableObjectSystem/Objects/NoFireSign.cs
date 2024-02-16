using System.Collections;
using System.Collections.Generic;
using Interactions;
using Items;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class NoFireSign : InteractableObject {
        [SerializeField] private ItemSO _interactedWith;

        public override bool InteractionItem(Item item) {
            if (item.ItemSO == _interactedWith) {
                RaiseAlarm();
                return true;
            }

            return false;
        }

        private void RaiseAlarm() {
            
        }
    }
}
