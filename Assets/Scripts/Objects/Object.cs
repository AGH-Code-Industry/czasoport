using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions.Interfaces;
using CoinPackage.Debugging;
using Items;

namespace Objects {
    public abstract class Object : MonoBehaviour, IInteractableItem {

        public virtual void InteractionItem(Item item) {
            CDebug.LogError("Not implemented InteractionItem");
        }
    }
}
