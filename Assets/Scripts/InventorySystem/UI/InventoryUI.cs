using System;
using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;
using InventorySystem.EventArguments;
using Settings;

namespace InventorySystem.UI {
    /// <summary>
    /// Manages UI of inventory slots.
    /// </summary>
    public class InventoryUI : MonoBehaviour {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform container;
        [SerializeField] private Sprite hand;

        private List<Slot> _slots;

        private int _activeId = 0;

        private void Start() {
            _slots = new List<Slot>();

            for (int i = 0; i < DeveloperSettings.Instance.invSettings.itemsCount; i++) {
                GameObject slot = Instantiate(slotPrefab, container);
                RectTransform rT = slot.GetComponent<RectTransform>();
                rT.anchoredPosition = new Vector2(-51f + 86f * i, 30f);
                _slots.Add(slot.GetComponent<Slot>());
                _slots[i].RemoveItem();
                _slots[i].Disactive();
            }
            _slots[_activeId].Active();
            _slots[_activeId].AddItem(hand);
            // We need to subscribe to Inventory event in Start() script, because OnEnable() is run just
            // after Awake(). This means that Inventory might not be yet present (its Awake is run later),
            // and we will try to subscribe to its events.
            Inventory.Instance.SelectedSlotChanged += OnChangeSelectedSlot;
            Inventory.Instance.ItemInserted += OnItemAdded;
            Inventory.Instance.ItemRemoved += OnItemRemoved;
            Inventory.Instance.ItemStateChanged += OnItemChangeState;
        }

        /// <summary>
        /// Highlight choosed slot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnChangeSelectedSlot(object sender, SelectedSlotChangedEventArgs args) {
            if (args.Slot != _activeId) {
                _slots[_activeId].Disactive();
                _activeId = args.Slot;
                _slots[_activeId].Active();
            }
        }

        /// <summary>
        /// Change slot's image and durability.
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// </summary>
        public void OnItemChangeState(object sender, ItemStateChangedEventArgs args) {
            if (args.Item.Durability > 0) {
                _slots[args.Slot].AddItem(args.Item.ItemSO.image);
                _slots[args.Slot].SetDurability(args.Item.Durability);
            }
            else _slots[args.Slot].RemoveItem();
        }

        /// <summary>
        /// Change slot's image to newItem and set durability.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnItemAdded(object sender, ItemInsertedEventArgs args) {
            _slots[args.Slot].AddItem(args.Item.ItemSO.image);
            _slots[args.Slot].SetDurability(args.Item.Durability);
        }

        /// <summary>
        /// Change slot's image.apha to 0 and set text responsible for durability to "".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnItemRemoved(object sender, ItemRemovedEventArgs args) {
            _slots[args.Slot].RemoveItem();
        }
    }
}