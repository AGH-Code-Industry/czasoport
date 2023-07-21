using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem {
    public class InputTest : MonoBehaviour
    {
        private void Start() {
            CInput.InputActions.Inventory.ChooseItem.performed += OnChooseSlot;
            CInput.InputActions.Inventory.NextItem.performed += OnNextItem;
        }

        private void OnChooseSlot(InputAction.CallbackContext ctx) {
            CDebug.Log((int)ctx.ReadValue<float>());
        }

        private void OnNextItem(InputAction.CallbackContext ctx) {
            CDebug.Log("Next item" % Colorize.Blue);
        }
    }
}

