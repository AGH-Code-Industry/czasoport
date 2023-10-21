using System;
using UnityEngine;
using Interactions.Interfaces;
using CoinPackage.Debugging;
using Items;
using Application;
using Interactions;


namespace InteractableObjectSystem {
    [RequireComponent(typeof(HighlightInteraction))]
    public abstract class InteractableObject : MonoBehaviour, IItemInteractable, IHandInteractable, ILongHandInteractable, ILongItemInteractable {

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.INTERACTABLE_OBJECTS];

        public virtual bool InteractionItem(Item item) {
            CDebug.LogError("Not implemented InteractionItem");
            return false;
        }
        
        public virtual bool LongInteractionItem(Item item) {
            CDebug.LogError("Not implemented LongInteractionItem");
            return false;
        }
        
        public virtual void InteractionHand() {
            CDebug.LogError("Not implemented InteractionHand");
        }
        
        public virtual void LongInteractionHand() {
            CDebug.LogError("Not implemented LongInteractionHand");
        }
    }
}
