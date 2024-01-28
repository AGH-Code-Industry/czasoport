using System.Collections.Generic;
using CoinPackage.Debugging;
using InventorySystem;
using Items;
using UnityEngine;
using InteractableObjectSystem;
using Notifications;
using UnityEngine.Serialization;

namespace NPC {
    public class Trading : InteractableObject {
        [SerializeField] private List<GameObject> contains;
        [SerializeField] private List<ItemSO> interactedWith;

        private PathWalking _walking;
        private bool _canWalk;

        private void Start() {
            _walking = GetComponent<PathWalking>();
            if (_walking != null) _canWalk = true;
        }

        public override void InteractionHand() {
            if (_canWalk) {
                _walking.StopWalk();
            }
            if (interactedWith.Count == 0) {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.successfulHandInterNotification.message,definition.successfulHandInterNotification.displayTime));
                if(_canWalk) _walking.Invoke("ContinueWalk",definition.successfulHandInterNotification.displayTime);
                Give();
            }
            else {
                NotificationManager.Instance.RaiseNotification(new Notification(
                    definition.failedHandInterNotification.message,
                    definition.failedHandInterNotification.displayTime));
                if (_canWalk) _walking.Invoke("ContinueWalk",definition.failedHandInterNotification.displayTime);
            }
            
        }

        public override bool InteractionItem(Item item) {
            if (_canWalk && _walking != null) _walking.StopWalk();
            
            if (interactedWith.Count == 0 || interactedWith.Contains(item.ItemSO)) {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.successfulItemInterNotification.message,definition.successfulItemInterNotification.displayTime));
                if (interactedWith.Count != 0) Inventory.Instance.RemoveItem(out Item i);
                if(_canWalk) _walking.Invoke("ContinueWalk",definition.successfulItemInterNotification.displayTime);
                Give();
                return true;
            } else {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.failedItemInterNotification.message, definition.failedItemInterNotification.displayTime));
                if (_canWalk) _walking.Invoke("ContinueWalk", definition.failedItemInterNotification.displayTime);
            }
            return false;
        }

        private void Give() {
            CDebug.Log("Trading");
            while (contains.Count > 0) {
                Item i = contains[0].GetComponent<Item>();
                if (Inventory.Instance.InsertItem(i)) {
                    contains.RemoveAt(0);
                }
                else return;

            }
            CDebug.Log("NPC doesn't have anything");
        }
    }
}