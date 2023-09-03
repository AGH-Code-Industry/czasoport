using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using CustomInput;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory {
    public class Inventory : MonoBehaviour {
        public static Inventory Instance;

        public delegate void SelectedSlotChangedDelegate(int slot);
        public SelectedSlotChangedDelegate SelectedSlotChanged;
        
        public delegate void ItemInsertedDelegate(int slot, Item item);
        public ItemInsertedDelegate ItemInserted;

        public delegate void ItemRemovedDelegate(int slot, Item item);
        public ItemRemovedDelegate ItemRemoved;

        public delegate void ItemStateChangedDelegate(int slot, Item item);
        public ItemStateChangedDelegate ItemStateChanged;

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

            SelectedSlotChanged += slot =>
                _logger.Log($"Selected slot changed, new slot: {slot % Colorize.Magenta}.");
            ItemInserted += (slot, item) =>
                _logger.Log($"Item {item % Colorize.Cyan} {"inserted" % Colorize.Green} into the {slot % Colorize.Magenta} slot.");
            ItemRemoved += (slot, item) => 
                _logger.Log($"Item {item % Colorize.Cyan} {"removed" % Colorize.Red} from the {slot % Colorize.Magenta} slot.");
            ItemStateChanged += (slot, item) =>
                _logger.Log($"Item {item % Colorize.Cyan} state {"changed" % Colorize.Orange}, {slot % Colorize.Magenta} slot.");
            
            _items = new Item[settings.itemsCount];
        }

        private void OnEnable() {
            CInput.InputActions.Inventory.NextItem.performed += OnNextItemClicked;
            CInput.InputActions.Inventory.PreviousItem.performed += OnPreviousItemClicked;
            CInput.InputActions.Inventory.ChooseItem.performed += OnChooseItemClicked;
            CInput.InputActions.Inventory.Drop.performed += OnDropItemClicked;
        }

        private void OnDisable() {
            CInput.InputActions.Inventory.NextItem.performed -= OnNextItemClicked;
            CInput.InputActions.Inventory.PreviousItem.performed -= OnPreviousItemClicked;
            CInput.InputActions.Inventory.ChooseItem.performed -= OnChooseItemClicked;
            CInput.InputActions.Inventory.Drop.performed -= OnDropItemClicked;
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
                ItemInserted(_selectedSlot, item);
                return true;
            }
            // Put item into first empty slot
            for (int i = 0; i < settings.itemsCount; i++) {
                if (_items[i] is null) {
                    _items[i] = item;
                    _itemsCount++;
                    ItemInserted(i, item);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieve and remove item from the Inventory. Empty slot is always the result.
        /// </summary>
        /// <param name="item">Removed item if selected slot was not empty.</param>
        /// <returns>`True` if item was removed, `false` if slot was empty. If `false`, retrieved item will be null.</returns>
        public bool RemoveItem(out Item item) {
            item = _items[_selectedSlot];
            _items[_selectedSlot] = null;
            ItemRemoved(_selectedSlot, item);
            return item is not null;
        }

        private void OnNextItemClicked(InputAction.CallbackContext ctx) {
            _selectedSlot++;
            _selectedSlot = _selectedSlot < settings.itemsCount ? _selectedSlot : 0;
            SelectedSlotChanged(_selectedSlot);
        }
        
        private void OnPreviousItemClicked(InputAction.CallbackContext ctx) {
            _selectedSlot--;
            _selectedSlot = _selectedSlot < 0 ? settings.itemsCount - 1 : _selectedSlot;
            SelectedSlotChanged(_selectedSlot);
        }
        
        private void OnChooseItemClicked(InputAction.CallbackContext ctx) {
            _selectedSlot = Math.Clamp((int)ctx.ReadValue<float>(), 0, _items.Length - 1);
            SelectedSlotChanged(_selectedSlot);
        }
        
        private void OnDropItemClicked(InputAction.CallbackContext ctx) {
            
        }
    }
}