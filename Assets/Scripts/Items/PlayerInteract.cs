using CustomInput;
using Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items {
    public class PlayerInteract : MonoBehaviour {
        public static PlayerInteract Instance {get; private set;}

        [SerializeField] private Item focusedItem;
        [SerializeField] private Objects.Object focusedObject;

        private Item holdingItem;

        private void Awake() {
            Instance = this;
        }

        private void OnEnable() {
            CInput.InputActions.Interactions.Interaction.performed += Interaction_performed;
            CInput.InputActions.Interactions.ItemInteraction.performed += ItemInteraction_performed;
        }

        private void OnDisable() {
            CInput.InputActions.Interactions.Interaction.performed -= Interaction_performed;
            CInput.InputActions.Interactions.ItemInteraction.performed -= ItemInteraction_performed;
        }

        private void ItemInteraction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            focusedObject.InteractionItem(holdingItem);
        }

        private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            focusedItem.GetItem();
        }

        public void SetHoldingItem(Item item) {
            holdingItem = item;
        }
    }
}
