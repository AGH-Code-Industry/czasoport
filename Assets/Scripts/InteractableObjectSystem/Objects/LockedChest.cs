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
        
        [SerializeField] private List<GameObject> contains;
        [SerializeField] private List<ItemSO> interactedWith;
        
        [SerializeField] private ChestState state = ChestState.Locked;
        

        public override void InteractionHand() {
            if (state == ChestState.Locked) {
                NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
                return;
            }
            else {
                NotificationManager.Instance.RaiseNotification(definition.successfulHandInterNotification);
                OpenChest();
            }
        }

        public override bool InteractionItem(Item item) {
            if (state != ChestState.Locked) {
                return false;
            }
            if (interactedWith.Contains(item.ItemSO)) {
                UnlockChest();
                NotificationManager.Instance.RaiseNotification(definition.successfulItemInterNotification);
                return true;
            }
            NotificationManager.Instance.RaiseNotification(definition.failedItemInterNotification);
            return false;
        }

        private void UnlockChest() {
            state = ChestState.Unlocked;
            OpenChest();
            //CDebug.Log("Unlocked");
        }

        private void OpenChest() {
            //CDebug.Log("Opened");
            while (contains.Count > 0) {
                GameObject c = contains[0];
                Item i = c.GetComponent<Item>();
                if (Inventory.Instance.InsertItem(i)) {
                    contains.RemoveAt(0);
                }
                else return;

            }
            //CDebug.Log("Chest is empty");
        }
    }
}
