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

        [SerializeField] protected InterObjectDefinition definition;

        protected readonly CLogger Logger = Loggers.LoggersList[Loggers.LoggerType.INTERACTABLE_OBJECTS];

        public virtual bool InteractionItem(Item item) {
            Logger.LogError("Not implemented InteractionItem", gameObject);
            return false;
        }

        public virtual bool LongInteractionItem(Item item) {
            Logger.LogError("Not implemented LongInteractionItem", gameObject);
            return false;
        }

        public virtual void InteractionHand() {
            Logger.LogError("Not implemented InteractionHand", gameObject);
        }

        public virtual void LongInteractionHand() {
            Logger.LogError("Not implemented LongInteractionHand", gameObject);
        }
    }
}