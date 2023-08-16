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
        [SerializeField] private float slotMarginX;
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
                slot.transform.position = new Vector3((slotW * (1 + 2*i)) + (slotMarginX * (i+1)), slotRealMarginY, 0);
                _slots.Add(slot.GetComponent<Slot>());
                _slots[i].RemoveItem();
                _slots[i].Disactive();
            }
            _slots[_activeId].Active();
        }

        /// <summary>
        /// Highlight choosed slot or/and set durabilty for active slot.
        /// </summary>
        /// <param name="newId"></param>
        public void OnItemStateChange(int id, int new_durability = -1) {
            if (id != _activeId) {
                _slots[_activeId].Disactive();
                _activeId = id;
                _slots[_activeId].Active();
            }
            if (new_durability != -1){
                _slots[id].SetDurabilty(new_durability);
            }
        }

        /// <summary>
        /// Change slot's image to newItem and set durability.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newItem"></param>
        public void OnItemAdded(int id, Sprite newItem, int durability) {
            _slots[id].AddItem(newItem);
            _slots[id].SetDurabilty(durability);
        }

        /// <summary>
        /// Change slot's image.apha to 0 and set text responsible for durability to "".
        /// </summary>
        /// <param name="id"></param>
        public void OnItemRemoved(int id) {
            _slots[id].RemoveItem();
        }
    }
}