using System.Collections.Generic;
using CoinPackage.Debugging;
using InventorySystem;
using Items;
using UnityEngine;
using InteractableObjectSystem;
using Notifications;
using UnityEngine.Serialization;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Fireplace : InteractableObject {

        [SerializeField] private List<GameObject> contains;
        [SerializeField] private List<ItemSO> interactedWith;

        public override void InteractionHand() {
            if (interactedWith.Count == 0) {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.successfulHandInterNotification.message, definition.successfulHandInterNotification.displayTime));
                Give();
            }
            else {
                NotificationManager.Instance.RaiseNotification(new Notification(
                    definition.failedHandInterNotification.message,
                    definition.failedHandInterNotification.displayTime));
            }

        }

        public override bool InteractionItem(Item item) {
            if (interactedWith.Contains(item.ItemSO)) {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.successfulItemInterNotification.message, definition.successfulItemInterNotification.displayTime));
                Inventory.Instance.RemoveItem(out Item i);
                Give();
                return true;
            }
            else {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.failedItemInterNotification.message, definition.failedItemInterNotification.displayTime));
            }
            return false;
        }

        private void Give() {
            foreach (GameObject p in contains) {
                GameObject gO = Instantiate(p);
                Item i = gO.GetComponent<Item>();
                Inventory.Instance.InsertItem(i);
            }
        }
    }
}