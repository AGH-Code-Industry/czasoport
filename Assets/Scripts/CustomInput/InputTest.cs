using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomInput.Interactions;

namespace CustomInput {
    public class InputTest : MonoBehaviour {
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Register() {
            InputSystem.RegisterInteraction<CustomHold>();
        }

        private void OnEnable() {
            CInput.InputActions.Inventory.ChooseItem.performed += OnChooseSlot;
            CInput.InputActions.Inventory.NextItem.performed += OnNextItem;
            CInput.InputActions.Mouse.LeftClick.performed += OnPrimaryMouse;
            CInput.InputActions.Interactions.Interaction.performed += OnInteraction;
            CInput.InputActions.Interactions.LongInteraction.performed += OnLongInteractionPerfomed;
            CInput.InputActions.Interactions.LongInteraction.canceled += OnLongInteractionCancelled;
        }

        private void OnDisable() {
            CInput.InputActions.Inventory.ChooseItem.performed -= OnChooseSlot;
            CInput.InputActions.Inventory.NextItem.performed -= OnNextItem;
            CInput.InputActions.Mouse.LeftClick.performed -= OnPrimaryMouse;
            CInput.InputActions.Interactions.Interaction.performed -= OnInteraction;
            CInput.InputActions.Interactions.LongInteraction.performed -= OnLongInteractionPerfomed;
            CInput.InputActions.Interactions.LongInteraction.canceled -= OnLongInteractionCancelled;
        }

        private void OnPrimaryMouse(InputAction.CallbackContext ctx) {
            CDebug.Log(ctx);
            CDebug.Log(CInput.MouseWorldPosition);
        }

        private void OnChooseSlot(InputAction.CallbackContext ctx) {
            CDebug.Log((int)ctx.ReadValue<float>());
        }

        private void OnNextItem(InputAction.CallbackContext ctx) {
            CDebug.Log("Next item" % Colorize.Cyan);
        }

        private void OnInteraction(InputAction.CallbackContext ctx) {
            CDebug.Log("Tap interaction");
        }

        private void OnLongInteractionPerfomed(InputAction.CallbackContext ctx) {
            CDebug.Log("Start holding interaction");
        }

        private void OnLongInteractionCancelled(InputAction.CallbackContext ctx) {
            CDebug.Log("Stop holding interaction");
        }
    }
}