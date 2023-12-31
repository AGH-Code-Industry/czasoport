using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockedDoor : InteractableObject {
        
        private enum DoorState {
            Locked,
            Closed,
            Opened
        }
        
        [SerializeField] private List<ItemSO> interactedWith;

        private BoxCollider2D _collider;
        private DoorState _state;

        private void Awake() {
            _collider = GetComponent<BoxCollider2D>();
            _state = DoorState.Locked;
        }

        public override void InteractionHand() {
            if (_state == DoorState.Locked) {
                NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
                return;
            }

            if (_state == DoorState.Opened) {
                CloseDoor();
            }
            else {
                OpenDoor();
            }
        }

        public override bool InteractionItem(Item item) {
            if (_state != DoorState.Locked) {
                return false;
            }
            if (interactedWith.Contains(item.ItemSO)) {
                OpenDoor();
                NotificationManager.Instance.RaiseNotification(definition.successfulItemInterNotification);
                return true;
            }
            NotificationManager.Instance.RaiseNotification(definition.failedItemInterNotification);
            return false;
        }

        private void UnlockDoor() {
            _state = DoorState.Opened;
            _collider.enabled = false;
            CDebug.Log("Unlocked");
        }

        private void OpenDoor() {
            _state = DoorState.Opened;
            _collider.enabled = false;
            CDebug.Log("Opened");
        }

        private void CloseDoor() {
            _state = DoorState.Closed;
            _collider.enabled = true;
            CDebug.Log("Closed");
        }
    }
}
