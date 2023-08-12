using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI {
    /// <summary>
    /// UI slot class.
    /// </summary>
    public class Slot : MonoBehaviour {
        private Image _frame;
        private Image _item;

        private void Start() {
            _item = transform.Find("Item").GetComponent<Image>();
            _frame = transform.Find("Frame").GetComponent<Image>();
        }
        
        /// <summary>
        /// Add item to slot
        /// </summary>
        /// <param name="newItem"></param>
        public void AddItem(Image newItem) {
            _item = newItem;
            var tempColor = _item.color;
            tempColor.a = 1f;
            _item.color = tempColor;
        }

        /// <summary>
        /// Remove item from slot
        /// </summary>
        public void RemoveItem() {
            var tempColor = _item.color;
            tempColor.a = 0f;
            _item.color = tempColor;
        }

        /// <summary>
        /// Active slot's frame
        /// </summary>
        public void Active() {
            var tempColor = _frame.color;
            tempColor.a = 1f;
            _frame.color = tempColor;
        }

        /// <summary>
        /// Disactive slot's frame
        /// </summary>
        public void Disactive() {
            var tempColor = _frame.color;
            tempColor.a = 0f;
            _frame.color = tempColor;
        }
    }
}