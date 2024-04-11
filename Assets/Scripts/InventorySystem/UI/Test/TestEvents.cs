using CustomInput;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace InventorySystem.UI.Test {
    /// <summary>
    /// Script to test InventoryUI
    /// </summary>
    public class TestEvents : MonoBehaviour {
        [SerializeField] private InventoryUI InventoryUIManager;
        [SerializeField] private Sprite test;
        private int _activeId = 0;

        private void OnEnable() {
            CInput.InputActions.Inventory.PreviousItem.performed += PreviousItem;
            CInput.InputActions.Inventory.NextItem.performed += NextItem;
            CInput.InputActions.Inventory.Drop.performed += DropItem;
            CInput.InputActions.Inventory.ChooseItem.performed += ChangeItemState;
        }

        private void OnDisable() {
            CInput.InputActions.Inventory.PreviousItem.performed -= PreviousItem;
            CInput.InputActions.Inventory.NextItem.performed -= NextItem;
            CInput.InputActions.Inventory.Drop.performed -= DropItem;
        }

        private void PreviousItem(InputAction.CallbackContext ctx) {
            _activeId--;
            if (_activeId == -1) _activeId = 3;
            //InventoryUIManager.OnItemRemoved(_activeId);
            //InventoryUIManager.OnItemStateChange(_activeId);
        }

        private void NextItem(InputAction.CallbackContext ctx) {
            _activeId++;
            if (_activeId == 4) _activeId = 0;
            //InventoryUIManager.OnItemAdded(_activeId, test, _activeId);
            //InventoryUIManager.OnItemStateChange(_activeId);
        }

        private void DropItem(InputAction.CallbackContext ctx) {
            //InventoryUIManager.OnItemRemoved(_activeId);   
        }

        private void ChangeItemState(InputAction.CallbackContext ctx) {
            //_activeId = (int)ctx.ReadValue<float>() - 1;
            //InventoryUIManager.OnItemStateChange(_activeId);
        }
    }
}