using CustomInput;
using UnityEngine;

namespace Inventory.UI.Test
{
    /// <summary>
    /// Script to test InventoryUI
    /// </summary>
    public class TestEvents : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        /*
         *         private void OnEnable()
            {
                CInput.InputActions.Inventory.PreviousItem.performed += PreviousItem;
                CInput.InputActions.Inventory.NextItem.performed += NextItem;
            }
    
            private void OnDisable() {
                CInput.InputActions.Inventory.PreviousItem.performed -= PreviousItem;
                CInput.InputActions.Inventory.NextItem.performed -= NextItem;
            }
    
            private void PreviousItem(InputAction.CallbackContext ctx) {
                ChooseItem(-1);
            }
    
            private void NextItem(InputAction.CallbackContext ctx) {
                ChooseItem(1);
            }
            
        */
        // Update is called once per frame
        void Update()
        {

        }
    }
}