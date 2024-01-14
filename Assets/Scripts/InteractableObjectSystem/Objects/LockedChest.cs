using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using InventorySystem;
using Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockedChest : InteractableObject {
        
        private enum ChestState {
            Locked,
            Unlocked
        }
        
        [SerializeField] private List<GameObject> _contains;
        [SerializeField] private List<ItemSO> _interactedWith;
        
        [SerializeField] private ChestState _state = ChestState.Locked;
        

        public override void InteractionHand() {
            if (_state == ChestState.Locked) {
                NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
                return;
            }
            else {
                NotificationManager.Instance.RaiseNotification(definition.successfulHandInterNotification);
                OpenChest();
            }
        }

        public override bool InteractionItem(Item item) {
            if (_state != ChestState.Locked) {
                return false;
            }
            if (_interactedWith.Contains(item.ItemSO)) {
                UnlockChest();
                NotificationManager.Instance.RaiseNotification(definition.successfulItemInterNotification);
                return true;
            }
            NotificationManager.Instance.RaiseNotification(definition.failedItemInterNotification);
            return false;
        }

        private void UnlockChest() {
            _state = ChestState.Unlocked;
            OpenChest();
            CDebug.Log("Unlocked");
        }

        private void OpenChest() {
            CDebug.Log("Opened");
            while (_contains.Count > 0) {
                GameObject c = _contains[0];
                Item i = c.GetComponent<Item>();
                if (Inventory.Instance.InsertItem(i)) {
                    _contains.RemoveAt(0);
                }
                else return;

            }
            CDebug.Log("Chest is empty");
        }
    }
}
