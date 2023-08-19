using System;
using System.Collections.Generic;
using CustomInput;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions {
    public class Interactions : MonoBehaviour {
        public static Interactions Instance;

        private List<GameObject> _interactableObjects;
        [CanBeNull] private GameObject _selectedObject = null;
        [CanBeNull] private GameObject _focusedObject = null;

        private void Awake() {
            _interactableObjects = new List<GameObject>();
        }

        private void OnEnable() {
            CInput.InputActions.Interactions.Interaction.performed += OnInteractionPerformed;
            CInput.InputActions.Interactions.LongInteraction.performed += OnLongInteractionPerformed;
            CInput.InputActions.Interactions.ItemInteraction.performed += OnItemInteractionPerformed;
            CInput.InputActions.Interactions.LongItemInteraction.performed += OnLongItemInteractionPerformed;
        }
        
        [Obsolete]
        public void RegisterInteractable(GameObject interactable) {
            _interactableObjects.Add(interactable);
        }

        [Obsolete]
        public void UnregisterInteractable(GameObject interactable) {
            _interactableObjects.Remove(interactable);
            if (_interactableObjects.Count == 0) {
                _selectedObject = null;
                _focusedObject = null;
                return;
            }

            if (_focusedObject == interactable) {
                _focusedObject = null;
                _selectedObject = GetNearestInteractable();
            }
        }

        private void UpdateInteractables() {
            if (_focusedObject is null) {
                _selectedObject = GetNearestInteractable();
            }
            else {
                _selectedObject = _focusedObject;
            }
        }

        [CanBeNull]
        private GameObject GetNearestInteractable() {
            GameObject? nearest = null;
            var distance = 99999f;
            foreach (var interactable in _interactableObjects) {
                var dist = Vector2.Distance(transform.position, interactable.transform.position);
                if (dist < distance) {
                    nearest = interactable;
                    distance = dist;
                }
            }
            return nearest;
        }

        private void OnInteractionPerformed(InputAction.CallbackContext ctx) {
            
        }
        
        private void OnLongInteractionPerformed(InputAction.CallbackContext ctx) {
            
        }
        
        private void OnItemInteractionPerformed(InputAction.CallbackContext ctx) {
            
        }
        
        private void OnLongItemInteractionPerformed(InputAction.CallbackContext ctx) {
            
        }
    }
}