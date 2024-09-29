using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.Serialization;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockDoor : MonoBehaviour {

        public enum DoorLockState {
            Locked,
            Closed,
            Opened
        }

        [SerializeField] private float openingSpeed;
        [SerializeField] private float _openingDelay;

        [SerializeField] private AudioSource doorAudioSource; // Add AudioSource field - Kasia Psuje
        public EventHandler doorsOpened;
        public EventHandler doorsClosed;

        [SerializeField] private BoxCollider2D collider;
        [SerializeField] private Animator animator;

        public void UnlockDoor() {
            lockState = DoorLockState.Opened;
            collider.enabled = false;
            CDebug.Log("Unlocked");
        }

        public void ToLockDoor() {
            lockState = DoorLockState.Locked;
            collider.enabled = true;
            CDebug.Log("Locked");
        }

        public void OpenDoor() {
            if (lockState == DoorLockState.Opened) {
                CloseDoor();
                return;
            }
            animator.SetTrigger("OpenDoors");
            CDebug.Log("Opened");
            lockState = DoorLockState.Opened;
            StartCoroutine(OpenDoortsWithDelay(_openingDelay));

            doorsOpened?.Invoke(this, EventArgs.Empty);
        }

        IEnumerator OpenDoortsWithDelay(float delay) {
            yield return new WaitForSeconds(delay);
            collider.enabled = false;
            animator.ResetTrigger("CloseDoors");
            // Play sound - Kasia Psuje
            if (doorAudioSource != null && doorAudioSource.clip != null) { //- Kasia Psuje
                doorAudioSource.Play(); //- Kasia Psuje
            } //- Kasia Psuje
        }

        public void CloseDoor() {
            if (lockState == DoorLockState.Closed) {
                OpenDoor();
                return;
            } else if (lockState == DoorLockState.Locked) return;
            Debug.Log("Closing");
            animator.SetTrigger("CloseDoors");
            lockState = DoorLockState.Closed;
            collider.enabled = true;
            CDebug.Log("Closed");
            animator.ResetTrigger("OpenDoors");

            doorsClosed?.Invoke(this, EventArgs.Empty);
        }

    }
}