using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem {
    public class InputTest : MonoBehaviour
    {
        private void OnEnable() {
            CInput.InputActions.Inventory.ChooseItem.performed += OnChooseSlot;
            CInput.InputActions.Inventory.NextItem.performed += OnNextItem;
            CInput.InputActions.Mouse.LeftClick.performed += OnPrimaryMouse;
        }

        private void OnDisable() {
            CInput.InputActions.Inventory.ChooseItem.performed -= OnChooseSlot;
            CInput.InputActions.Inventory.NextItem.performed -= OnNextItem;
            CInput.InputActions.Mouse.LeftClick.performed -= OnPrimaryMouse;
        }

        private void OnPrimaryMouse(InputAction.CallbackContext ctx) {
            CDebug.Log(ctx);
            CDebug.Log(CInput.MousePosition);
        }

        private void OnChooseSlot(InputAction.CallbackContext ctx) {
            CDebug.Log((int)ctx.ReadValue<float>());
        }

        private void OnNextItem(InputAction.CallbackContext ctx) {
            CDebug.Log("Next item" % Colorize.Cyan);
        }
    }
}

