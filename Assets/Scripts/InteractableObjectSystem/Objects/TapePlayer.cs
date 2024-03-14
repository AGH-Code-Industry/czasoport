using System.Collections;
using System.Collections.Generic;
using Interactions;
using Items;
using NPC;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class TapePlayer : InteractableObject {
        [SerializeField] private ItemSO _interactedWith;
        [SerializeField] private List<PathWalking> _NPCPathWalkings = new ();

        public override void InteractionHand() {
            NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
        }

        public override bool InteractionItem(Item item) {
            if (item.ItemSO == _interactedWith) {
                PlayMusic();
                return true;
            }

            return false;
        }

        private void PlayMusic() {
            foreach (var pathWalking in _NPCPathWalkings) {
                pathWalking.StartWalk();
            }
        }
    }
}