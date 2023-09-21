using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Items;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    public class LockedDoor : InteractableObject {
        
        private enum DoorState {
            Locked,
            Unlocked
        }
        
        [SerializeField] private List<Item> _keys = new();

        private DoorState state;

        private void Awake() {
            state = DoorState.Locked;
        }

        public override bool InteractionItem(Item item) {
            if (_keys.Contains(item)) {
                OpenDoor();
                return true;
            }
            CDebug.Log("Bad key");
            return false;
        }

        private void OpenDoor() {
            state = DoorState.Unlocked;
            CDebug.Log("Door unlocked");
        }
    }
}