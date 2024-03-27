using UnityEngine;
using UnityEngine.UI;
using CoinPackage.Debugging;
using TMPro;

namespace InventorySystem.UI {
    /// <summary>
    /// UI slot class.
    /// </summary>
    public class Slot : MonoBehaviour {
        [SerializeField] private Color32 activeSlot;
        [SerializeField] private Color32 disactiveSlot;
        private Image _image;
        private Image _item;
        private TextMeshProUGUI _text;

        private void Awake() {
            _image = GetComponent<Image>();
            _item = transform.Find("Item").GetComponent<Image>();
            _text = transform.Find("Durability").GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Add item to slot
        /// </summary>
        /// <param name="newItem"></param>
        public void AddItem(Sprite newItem) {
            _item.sprite = newItem;
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
            _text.text = "";
        }

        /// <summary>
        /// Active slot's frame
        /// </summary>
        public void Active() {
            _image.color = activeSlot;
        }

        /// <summary>
        /// Disactive slot's frame
        /// </summary>
        public void Disactive() {
            _image.color = disactiveSlot;
        }

        /// <summary>
        /// Change slot's text to new_durability or ""
        /// </summary>
        /// <param name="new_durability"></param>
        public void SetDurability(int new_durability) {
            if (new_durability > 1) {
                _text.text = new_durability.ToString();
            }
            else _text.text = "";
        }
    }
}