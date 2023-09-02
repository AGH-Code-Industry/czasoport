using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using CoinPackage.Debugging;
using CustomInput;
using Interactions.Interfaces;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions {
    public class Interactions : MonoBehaviour {
        public static Interactions Instance;

        [SerializeField] private InteractionsSettings settings;
        
        private HashSet<GameObject> _interactableObjects;
        [CanBeNull] private GameObject _selectedObject = null;
        [CanBeNull] private GameObject _focusedObject = null;
        private readonly CLogger _logger = Loggers.LoggersList["INTERACTIONS"];

        private float _lastInteractablesUpdate = 0;

        private void Awake() {
            _interactableObjects = new HashSet<GameObject>();
        }

        private void OnEnable() {
            CInput.InputActions.Movement.FocusChange.performed += OnFocusChangePerformed;
            CInput.InputActions.Interactions.Interaction.performed += OnInteractionPerformed;
            CInput.InputActions.Interactions.LongInteraction.performed += OnLongInteractionPerformed;
            CInput.InputActions.Interactions.ItemInteraction.performed += OnItemInteractionPerformed;
            CInput.InputActions.Interactions.LongItemInteraction.performed += OnLongItemInteractionPerformed;
        }

        private void OnDisable() {
            CInput.InputActions.Movement.FocusChange.performed -= OnFocusChangePerformed;
            CInput.InputActions.Interactions.Interaction.performed -= OnInteractionPerformed;
            CInput.InputActions.Interactions.LongInteraction.performed -= OnLongInteractionPerformed;
            CInput.InputActions.Interactions.ItemInteraction.performed -= OnItemInteractionPerformed;
            CInput.InputActions.Interactions.LongItemInteraction.performed -= OnLongItemInteractionPerformed;
        }

        private void Update() {
            UpdateInteractables();
            UpdateSelectedInteractables();
        }

        private void UpdateInteractables() {
            if (Time.time - _lastInteractablesUpdate < settings.interactionCheckInterval) {
                return;
            }
            _lastInteractablesUpdate = Time.time;
            
            List<Collider2D> result = new List<Collider2D>();
            Physics2D.OverlapCircle(transform.position,
                settings.defaultInteractionRadius,
                new ContactFilter2D() {
                    layerMask = LayerMask.GetMask(settings.interactablesLayer),
                    useLayerMask = true
                },
                result);
            var interactables = new HashSet<GameObject>(result.Select(x => x.gameObject));
            
            _interactableObjects.ExceptWith(interactables);
            foreach (var oldInteractable in _interactableObjects) {
                oldInteractable.GetComponent<IHighlightable>()?.DisableHighlight();
            }
            _interactableObjects = new HashSet<GameObject>(interactables);
            foreach (var interactableObject in _interactableObjects) {
                interactableObject.GetComponent<IHighlightable>()?.EnableHighlight();
            }

            // If focused object went out of reach, lose focus on it
            if (!_interactableObjects.Contains(_focusedObject)) {
                _focusedObject = null;
            }
        }

        private void UpdateSelectedInteractables() {
            if (_interactableObjects.Count == 0) {
                _focusedObject = null;
                _selectedObject = null;
                return;
            }

            if (_focusedObject is null) {
                _selectedObject = GetNearestInteractable();
                _selectedObject.GetComponent<IHighlightable>()?.EnableFocusedHighlight();
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

        private void OnFocusChangePerformed(InputAction.CallbackContext ctx) {
            
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