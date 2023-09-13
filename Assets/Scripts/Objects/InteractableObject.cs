using UnityEngine;
using Interactions.Interfaces;
using CoinPackage.Debugging;
using Items;
using Application;


namespace Objects {
    public abstract class InteractableObject : MonoBehaviour, IItemInteractable, IHandInteractable, ILongHandInteractable, ILongItemInteractable {

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.INTERACTABLE_OBJECTS];
        
        [SerializeField] private ObjectSO objectSO;
        
        public virtual void InteractionItem(Item item) {
            CDebug.LogError("Not implemented InteractionItem");
        }
        
        public virtual void LongInteractionItem(Item item) {
            CDebug.LogError("Not implemented LongInteractionItem");
        }
        
        public virtual void InteractionHand() {
            CDebug.LogError("Not implemented InteractionHand");
        }
        
        public virtual void LongInteractionHand() {
            CDebug.LogError("Not implemented LongInteractionHand");
        }
        
        public ObjectSO GetObjectSO()
        {
            return objectSO;
        }
    }
}
