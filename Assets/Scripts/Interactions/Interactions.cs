using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using CustomInput;
using DataPersistence;
using Interactions.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using InventorySystem;
using Items;
using Settings;
using UnityEngine.Serialization;

namespace Interactions {
    public class Interactions : MonoBehaviour {
        public static Interactions Instance;

        public event EventHandler itemInteractionPerformed;
        public event EventHandler handInteractionPerformed;
        public event EventHandler onFocusChange;

        // Objects near the player that we can interact with
        private List<GameObject> _interactableObjects;
        // Object that will be the subject of the interaction if _focusedObject is not selected
        [CanBeNull] private GameObject _selectedObject = null;
        // Object that will be the subject of the interaction if player used 'FocusChange' - specified object
        // he want to be focused on
        [CanBeNull] private GameObject _focusedObject = null;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.INTERACTIONS];
        private InteractionsSettingsSO _settings;
        private float _lastInteractablesUpdate = 0;

        private void Awake() {
            if (Instance != null) {
                _logger.LogError($"{this} tried to overwrite current singleton instance.", this);
                throw new SingletonOverrideException($"{this} tried to overwrite current singleton instance.");
            }
            Instance = this;
            _settings = DeveloperSettings.Instance.intSettings;
            _interactableObjects = new List<GameObject>();
        }

        private void OnEnable() {
            CInput.InputActions.Movement.FocusChange.performed += OnFocusChangePerformed;
            CInput.InputActions.Interactions.Interaction.performed += OnInteractionPerformed;
            CInput.InputActions.Interactions.LongInteraction.performed += OnLongInteractionPerformed;
        }

        private void OnDisable() {
            CInput.InputActions.Movement.FocusChange.performed -= OnFocusChangePerformed;
            CInput.InputActions.Interactions.Interaction.performed -= OnInteractionPerformed;
            CInput.InputActions.Interactions.LongInteraction.performed -= OnLongInteractionPerformed;
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
            if (Time.time - _lastInteractablesUpdate < _settings.interactionCheckInterval) {
                return;
            }
            _lastInteractablesUpdate = Time.time;

            List<Collider2D> result = new List<Collider2D>();
            Physics2D.OverlapCircle(transform.position,
                _settings.defaultInteractionRadius,
                new ContactFilter2D() {
                    layerMask = LayerMask.GetMask(_settings.interactablesLayer, _settings.itemsLayer),
                    useLayerMask = true,
                    useTriggers = true
                },
                result);
            var oldInteractables = new HashSet<GameObject>(_interactableObjects);
            var newInteractables = new HashSet<GameObject>(result.Select(x => x.gameObject));

            oldInteractables.ExceptWith(newInteractables);
            foreach (var oldInteractable in oldInteractables) {
                if (!oldInteractable) continue;
                oldInteractable.GetComponent<IHighlightable>()?.DisableHighlight();
            }
            _interactableObjects = new List<GameObject>(newInteractables);
            foreach (var interactableObject in _interactableObjects) {
                if (!interactableObject) continue;
                interactableObject.GetComponent<IHighlightable>()?.EnableHighlight();
            }

            // If focused object went out of reach, lose focus on it
            if (!_interactableObjects.Contains(_focusedObject)) {
                _focusedObject = null;
            }
        }

        void OnDrawGizmos() {
            if (UnityEngine.Application.isPlaying) {
                DrawInteractablesAreaGizmos();
                DrawLinesToInteractablesGizmos();
            }
        }

        private void DrawLinesToInteractablesGizmos() {
            foreach (var interactable in _interactableObjects) {
                if (!interactable) continue;
                Gizmos.DrawLine(transform.position, interactable.transform.position);
            }
        }

        private void DrawInteractablesAreaGizmos() {
            Gizmos.DrawWireSphere(transform.position, _settings.defaultInteractionRadius);
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
                GameObject g = GetNearestInteractable();
                if (_selectedObject != g) changeSelectedObject(g);
            }
            else {
                changeSelectedObject(_focusedObject);
            }

            if (!_selectedObject) return;
            // Despite warning, `_selectedObject` will never be null in this context
            _selectedObject.GetComponent<IHighlightable>()?.EnableFocusedHighlight();
        }

        private void changeSelectedObject(GameObject newSelectedObject) {
            if (_selectedObject is not null) _selectedObject.GetComponent<IHighlightable>()?.DisableFocusedHighlight();
            _selectedObject = newSelectedObject;
            onFocusChange?.Invoke(this, EventArgs.Empty);
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
                if (!interactable) continue;
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
                if (Inventory.Instance.GetSelectedItem(out var item)) {
                    var successful = _selectedObject.GetComponent<IItemInteractable>()?.InteractionItem(item);
                    if (successful == true) {
                        Inventory.Instance.UseItem();
                        itemInteractionPerformed?.Invoke(this, EventArgs.Empty);
                    }
                    else {
                        TryToInstertItem();
                    }
                }
                else {
                    _selectedObject.GetComponent<IHandInteractable>()?.InteractionHand();
                    handInteractionPerformed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void TryToInstertItem() {
            _selectedObject.GetComponent<Item>()?.InteractionHand();
        }

        private void OnLongInteractionPerformed(InputAction.CallbackContext ctx) {
            if (_selectedObject) {
                if (Inventory.Instance.GetSelectedItem(out var item)) {
                    _selectedObject.GetComponent<ILongItemInteractable>().LongInteractionItem(item);
                }
                else
                    _selectedObject.GetComponent<ILongHandInteractable>()?.LongInteractionHand();
            }
        }
    }
}