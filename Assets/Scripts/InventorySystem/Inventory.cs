using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using CustomInput;
using InventorySystem.EventArguments;
using Items;
using LevelTimeChange.LevelsLoader;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace InventorySystem {
    public class Inventory : MonoBehaviour {
        public static Inventory Instance;

        public event EventHandler<SelectedSlotChangedEventArgs> SelectedSlotChanged;
        public event EventHandler<ItemInsertedEventArgs> ItemInserted;
        public event EventHandler<ItemRemovedEventArgs> ItemRemoved;
        public event EventHandler<ItemStateChangedEventArgs> ItemStateChanged;
        
        public Transform itemHideout;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.INVENTORY];
        private InventorySettingsSO _settings;
        private Item[] _items;
        private int _itemsCount = 0;
        private int _selectedSlot = 0;

        private void Awake() {
            if (Instance != null) {
                _logger.LogError($"{this} tried to overwrite current singleton instance.", this);
                throw new SingletonOverrideException($"{this} tried to overwrite current singleton instance.");
            }

            Instance = this;
            _settings = DeveloperSettings.Instance.invSettings;

            SelectedSlotChanged += (sender, args) =>
                _logger.Log($"Selected slot changed, new slot: {args.Slot % Colorize.Magenta}.");
            ItemInserted += (sender, args) =>
                _logger.Log(
                    $"Item {args.Item.ItemSO.itemName % Colorize.Cyan} {"inserted" % Colorize.Green} into the {args.Slot % Colorize.Magenta} slot.");
            ItemRemoved += (sender, args) =>
                _logger.Log(
                    $"Item {args.Item.ItemSO.itemName % Colorize.Cyan} {"removed" % Colorize.Red} from the {args.Slot % Colorize.Magenta} slot.");
            ItemStateChanged += (sender, args) =>
                _logger.Log(
                    $"Item {args.Item.ItemSO.itemName % Colorize.Cyan} state {"changed" % Colorize.Orange}, {args.Slot % Colorize.Magenta} slot.");
            _items = new Item[_settings.itemsCount];
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
            if (_itemsCount == _settings.itemsCount) { // Inventory full
                return false;
            }

            if (_items[_selectedSlot] is null) { // Put item in the selected slot
                _items[_selectedSlot] = item;
                _itemsCount++;
                ItemInserted?.Invoke(this, new ItemInsertedEventArgs() {
                    Slot = _selectedSlot,
                    Item = item
                });
            }
            else { // Put item into first empty slot
                for (int i = 0; i < _settings.itemsCount; i++) {
                    if (_items[i] is null) {
                        _items[i] = item;
                        _itemsCount++;
                        ItemInserted?.Invoke(this, new ItemInsertedEventArgs() {
                            Slot = i,
                            Item = item
                        });
                        break;
                    }
                }
            }
            
            // Hide item
            item.transform.SetParent(itemHideout);
            item.transform.position = new Vector3(0f, 0f, 0f);
            item.GetComponent<SpriteRenderer>().enabled = false;
            item.GetComponent<CircleCollider2D>().enabled = false;
            
            NotificationManager.Instance.RaiseNotification(item.ItemSO.pickUpNotification);
            
            return true;
        }

        /// <summary>
        /// Changes durability of the selected item by one for each use. If durability is exactly 0,
        /// removes item from inventory.
        /// </summary>
        /// <returns>True if item was removed, false if not.</returns>
        public bool UseItem() {
            var item = _items[_selectedSlot];
            item.Durability -= 1;
            ItemStateChanged?.Invoke(this, new ItemStateChangedEventArgs() {
                Item = item,
                Slot = _selectedSlot
            });
            if (item.Durability == 0) {
                RemoveItem(out var removedItem);
                Destroy(item.gameObject);
                return true;
            }
            return true;
        }

        /// <summary>
        /// Retrieve and remove item from the Inventory. Empty slot is always the result.
        /// </summary>
        /// <param name="item">Removed item if selected slot was not empty.</param>
        /// <returns>`True` if item was removed, `false` if slot was empty. If `false`, retrieved item will be null.</returns>
        public bool RemoveItem(out Item item) {
            item = _items[_selectedSlot];
            _items[_selectedSlot] = null;
            ItemRemoved?.Invoke(this, new ItemRemovedEventArgs() {
                Slot = _selectedSlot,
                Item = item
            });
            return item is not null;
        }

        private void OnNextItemClicked(InputAction.CallbackContext ctx) {
            _selectedSlot++;
            _selectedSlot = _selectedSlot < _settings.itemsCount ? _selectedSlot : 0;
            SelectedSlotChanged?.Invoke(this, new SelectedSlotChangedEventArgs() {
                Slot = _selectedSlot
            });
        }

        private void OnPreviousItemClicked(InputAction.CallbackContext ctx) {
            _selectedSlot--;
            _selectedSlot = _selectedSlot < 0 ? _settings.itemsCount - 1 : _selectedSlot;
            SelectedSlotChanged?.Invoke(this, new SelectedSlotChangedEventArgs() {
                Slot = _selectedSlot
            });
        }

        private void OnChooseItemClicked(InputAction.CallbackContext ctx) {
            _selectedSlot = Math.Clamp((int)ctx.ReadValue<float>(), 0, _items.Length - 1);
            SelectedSlotChanged?.Invoke(this, new SelectedSlotChangedEventArgs() {
                Slot = _selectedSlot
            });
        }

        private void OnDropItemClicked(InputAction.CallbackContext ctx) {
            
        }
    }
}