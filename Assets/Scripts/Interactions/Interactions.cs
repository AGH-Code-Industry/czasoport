using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using CoinPackage.Debugging;
using CustomInput;
using Interactions.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions {
    public class Interactions : MonoBehaviour {
        public static Interactions Instance;

        [SerializeField] private InteractionsSettings settings;
        
        // Objects near the player that we can interact with
        private List<GameObject> _interactableObjects;
        // Object that will be the subject of the interaction if _focusedObject is not selected
        [CanBeNull] private GameObject _selectedObject = null;
        // Object that will be the subject of the interaction if player used 'FocusChange' - specified object
        // he want to be focused on
        [CanBeNull] private GameObject _focusedObject = null;
        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.INTERACTIONS];

        private float _lastInteractablesUpdate = 0;

        private void Awake() {
            _interactableObjects = new List<GameObject>();
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

        /// <summary>
        /// Get all interactable objects around the player, update available interactables
        /// and highlight available objects
        /// </summary>
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
                    useLayerMask = true,
                    useTriggers = true
                },
                result);
            var oldInteractables = new HashSet<GameObject>(_interactableObjects);
            var newInteractables = new HashSet<GameObject>(result.Select(x => x.gameObject));
            
            oldInteractables.ExceptWith(newInteractables);
            foreach (var oldInteractable in oldInteractables) {
                oldInteractable.GetComponent<IHighlightable>()?.DisableHighlight();
            }
            _interactableObjects = new List<GameObject>(newInteractables);
            foreach (var interactableObject in _interactableObjects) {
                interactableObject.GetComponent<IHighlightable>()?.EnableHighlight();
            }

            // If focused object went out of reach, lose focus on it
            if (!_interactableObjects.Contains(_focusedObject)) {
                _focusedObject = null;
            }
        }

        /// <summary>
        /// Update selected object
        /// </summary>
        private void UpdateSelectedInteractables() {
            if (_interactableObjects.Count == 0) {
                _focusedObject = null;
                _selectedObject = null;
                return;
            }

            if (_focusedObject is null) {
                _selectedObject = GetNearestInteractable();
            }
            else {
                _selectedObject = _focusedObject;
            }
            
            // Despite warning, `_selectedObject` will never be null in this context
            _selectedObject.GetComponent<IHighlightable>()?.EnableFocusedHighlight();
        }

        /// <summary>
        /// Get nearest object that can be interacted with
        /// </summary>
        /// <returns>Interactable or null if no object was found</returns>
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
            if (_focusedObject is null || _interactableObjects.Count < 2) {
                _focusedObject = _selectedObject;
                return;
            }

            var index = _interactableObjects.IndexOf(_selectedObject);
            index = index >= _interactableObjects.Count - 1 ? 0 : index + 1;
            _focusedObject = _interactableObjects[index];
            UpdateSelectedInteractables();
        }

        private void OnInteractionPerformed(InputAction.CallbackContext ctx) {
            if (_selectedObject) { // This is shortcut for checking if gameObject is not null
                _selectedObject.GetComponent<IHandInteractable>()?.InteractionHand();
            }
        }
        
        private void OnLongInteractionPerformed(InputAction.CallbackContext ctx) {
            if (_selectedObject) {
                _selectedObject.GetComponent<ILongHandInteractable>()?.LongInteractionHand();
            }
        }
        
        private void OnItemInteractionPerformed(InputAction.CallbackContext ctx) {
            if (!_selectedObject) {
                return;
            }
            // I dont know why Rider wants me to include namespace here, I can't use `using Inventory;` for some reason
            if (Inventory.Inventory.Instance.GetSelectedItem(out var item)) {
                _selectedObject.GetComponent<IItemInteractable>()?.InteractionItem(item);
            }
        }
        
        private void OnLongItemInteractionPerformed(InputAction.CallbackContext ctx) {
            if (!_selectedObject) {
                return;
            }

            if (Inventory.Inventory.Instance.GetSelectedItem(out var item)) {
                _selectedObject.GetComponent<ILongItemInteractable>().LongInteractionItem(item);
            }
        }
    }
}