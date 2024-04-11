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
        [SerializeField] private float openingSpeed;
        [SerializeField] private float _openingDelay;
        [SerializeField] private List<LockedDoor> doorsInOtherTimes;

        [SerializeField] private AudioSource doorAudioSource; // Add AudioSource field - Kasia Psuje


        private BoxCollider2D _collider;
        private BoxCollider2D _passage;
        private DoorState _state;
        private Animator _animator;

        private void Awake() {
            _passage = transform.parent.GetComponent<BoxCollider2D>();
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _state = DoorState.Locked;
            _passage.enabled = false;
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

        public void OpenDoor() {
            if (_state == DoorState.Opened)
                return;
            _passage.enabled = true;
            _animator.SetTrigger("OpenDoors");
            CDebug.Log("Opened");
            _state = DoorState.Opened;
            StartCoroutine(OpenDoortsWithDelay(_openingDelay));
            foreach (LockedDoor door in doorsInOtherTimes) {
                door.OpenDoor();
            }
        }

        IEnumerator OpenDoortsWithDelay(float delay) {
            yield return new WaitForSeconds(delay);
            _collider.enabled = false;
            _passage.enabled = false;
            _animator.ResetTrigger("CloseDoors");
            // Play sound - Kasia Psuje
            if (doorAudioSource != null && doorAudioSource.clip != null) { //- Kasia Psuje
                doorAudioSource.Play();//- Kasia Psuje
            }//- Kasia Psuje
        }

        public void CloseDoor() {
            if (_state == DoorState.Closed || _state == DoorState.Locked)
                return;
            Debug.Log("Closing");
            _animator.SetTrigger("CloseDoors");
            _state = DoorState.Closed;
            _collider.enabled = true;
            _passage.enabled = true;
            CDebug.Log("Closed");
            _animator.ResetTrigger("OpenDoors");
            foreach (LockedDoor door in doorsInOtherTimes) {
                door.OpenDoor();
            }
        }
    }
}