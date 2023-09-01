using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using Items;
using UnityEngine;

namespace Inventory {
    public class Inventory : MonoBehaviour {
        public static Inventory Instance;

        [SerializeField] private InventorySettings settings;
        
        private readonly CLogger _logger = Loggers.LoggersList["INVENTORY"];
        private Item[] _items;
        private int _itemsCount = 0;
        private int _selectedSlot = 0;

        private void Awake() {
            if (Instance != null) {
                _logger.LogError($"{this} tried to overwrite current singleton instance.", this);
                throw new SingletonOverrideException($"{this} tried to overwrite current singleton instance.");
            }
            Instance = this;

            _items = new Item[settings.itemsCount];
        }

        /// <summary>
        /// Get currently selected item, but do not remove this item from inventory.
        /// </summary>
        /// <param name="item">Retrieved item if slot was not empty.</param>
        /// <returns>`True` if slot was not empty, `false` if slot was empty. If empty, then `item` will be null.</returns>
        public bool GetSelectedItem(out Item item) {
            item = _items[_selectedSlot];
            return item is not null;
        }

        /// <summary>
        /// Get inventory, including empty slots, without removing items in it. Items are placed on their corresponding indexes.
        /// </summary>
        /// <returns>Array of `Item` objects including empty slots.</returns>
        public Item[] GetInventory() {
            return _items;
        }

        /// <summary>
        /// Get list of items in inventory without removing them. All null values are removed, so items won't keep
        /// their original indexes.
        /// </summary>
        /// <returns>List of Items, without including empty slots.</returns>
        public List<Item> GetAllItems() {
            var list = _items.ToList();
            list.RemoveAll(item => item is null);
            return list;
        }

        /// <summary>
        /// Insert item into inventory. If the selected slot is empty, then item is placed into that slot.
        /// </summary>
        /// <param name="item">`Item` to insert.</param>
        /// <returns></returns>
        public bool InsertItem(Item item) {
            if (_itemsCount == settings.itemsCount) { // Inventory full
                return false;
            }

            if (_items[_selectedSlot] is null) { // Put item in the selected slot
                _items[_selectedSlot] = item;
                _itemsCount++;
                return true;
            }
            // Put item into first empty slot
            for (int i = 0; i < settings.itemsCount; i++) {
                if (_items[i] is null) {
                    _items[i] = item;
                    _itemsCount++;
                    break;
                }
            }
            return true;
        }

        /// <summary>
        /// Retrieve and remove item from the Inventory.
        /// </summary>
        /// <param name="item">Removed item if selected slot was not empty.</param>
        /// <returns>`True` if item was removed, `false` if slot was empty. If `false`, retrieved item will be null.</returns>
        public bool RemoveItem(out Item item) {
            item = _items[_selectedSlot];
            _items[_selectedSlot] = null;
            return item is not null;
        }
    }
}