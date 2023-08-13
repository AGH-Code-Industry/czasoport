using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI {
    /// <summary>
    /// Manages UI of inventory slots.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private int slotCount;
        [SerializeField] private float slotMargintX;
        [SerializeField] private float slotMarginY;
        
        private List<Slot> _slots;
        
        private int _activeId = 0;

        private void Start() {
            _slots = new List<Slot>();
            GameObject slot;
            float slotW = (slotPrefab.GetComponent<RectTransform>().rect.width / 2);
            float slotRealMarginY = (slotPrefab.GetComponent<RectTransform>().rect.height / 2) + slotMarginY;
            for (int i = 0; i < slotCount; i++) {
                slot = Instantiate(slotPrefab, transform);
                slot.transform.position = new Vector3((slotW * (1 + 2*i)) + (slotMargintX * (i+1)), slotRealMarginY, 0);
                _slots.Add(slot.GetComponent<Slot>());
                _slots[i].RemoveItem();
                _slots[i].Disactive();
            }
            _slots[_activeId].Active();
        }

        /// <summary>
        /// Highlight choosed slot.
        /// </summary>
        /// <param name="newId"></param>
        public void OnItemStateChange(int newId) {   
            _slots[_activeId].Disactive();
            _activeId = newId;
            _slots[_activeId].Active();
        }

        /// <summary>
        /// Change slot's image to newItem.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newItem"></param>
        public void OnItemAdded(int id, Sprite newItem) {
            _slots[id].AddItem(newItem);
        }

        /// <summary>
        /// Change slot's image.apha to 0.
        /// </summary>
        /// <param name="id"></param>
        public void OnItemRemoved(int id) {
            _slots[id].RemoveItem();
        }
    }
}