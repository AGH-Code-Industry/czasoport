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
            Unlock,
            Locked,
            Closed,
            Opened
        }

        [SerializeField] private float openingDelay;

        [SerializeField] private AudioSource doorAudioSource; // Add AudioSource field - Kasia Psuje

        DoorLockState lockState;
        [SerializeField] private Animator animator;

        public void Start() {
            UnlockDoor();
        }

        public void UnlockDoor() {
            CDebug.Log("Unlocked");
            lockState = DoorLockState.Unlock;
            OpenDoor();
        }

        public void ToLockDoor() {
            CDebug.Log("Locked");
            lockState = DoorLockState.Locked;
            CloseDoor();
        }

        public void OpenDoor() {
            if (lockState == DoorLockState.Locked || lockState == DoorLockState.Opened) return;

            animator.SetBool("Open", true);

            CDebug.Log("Opened");
            lockState = DoorLockState.Opened;
            StartCoroutine(OpenDoortsWithDelay(openingDelay));
        }

        IEnumerator OpenDoortsWithDelay(float delay) {
            yield return new WaitForSeconds(delay);
            if (doorAudioSource != null && doorAudioSource.clip != null) {
                doorAudioSource.Play();
            }
        }

        public void CloseDoor() {
            if (lockState == DoorLockState.Closed) return;
            Debug.Log("Closing");

            animator.SetBool("Open", false);

            lockState = DoorLockState.Closed;
            CDebug.Log("Closed");
        }

    }
}