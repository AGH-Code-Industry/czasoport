using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using CustomInput;
using DataPersistence;
using DataPersistence.DataTypes;
using InventorySystem.EventArguments;
using Items;
using LevelTimeChange.LevelsLoader;
using LevelTimeChange.TimeChange;
using Notifications;
using PlayerScripts;
using Settings;
#if UNITY_EDITOR
using UnityEditor.UIElements;
using UnityEditor.Build;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace InventorySystem {
    public class Inventory : MonoBehaviour, IDataPersistence {
        public static Inventory Instance;

        public bool SceneObject { get; } = false;

        public event EventHandler<SelectedSlotChangedEventArgs> SelectedSlotChanged;
        public event EventHandler<ItemInsertedEventArgs> ItemInserted;
        public event EventHandler<ItemRemovedEventArgs> ItemRemoved;
        public event EventHandler<ItemStateChangedEventArgs> ItemStateChanged;

        public Transform itemHideout;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.INVENTORY];
        private InventorySettingsSO _settings;
        private Item[] _items;
        private int _itemsCount = 0;
        public int itemsCount {
            get {
                return _itemsCount;
            }
        }
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

        public void LoadPersistentData(GameData gameData) {
            foreach (var itemData in gameData.playerGameData.inventory) {
                var item = Instantiate(itemData.itemSO.prefab).GetComponent<Item>();
                item.Durability = itemData.durability;

                InsertItem(item);
            }
        }

        public void SavePersistentData(ref GameData gameData) {
            var items = GetAllItems();

            gameData.playerGameData.inventory.Clear();
            foreach (var item in items) {
                gameData.playerGameData.inventory.Add(new InventoryItemData {
                    itemSO = item.ItemSO,
                    durability = item.Durability
                });
            }
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
        /// Check if inventory contains item with the same `ItemSO` definition.
        /// </summary>
        /// <param name="itemSO">ItemDefinition of the item we want to check.</param>
        /// <returns></returns>
        public bool ContainsItem(ItemSO itemSO) {
            CDebug.Log($"Checking {itemSO}");
            return _items.Any(item => item != null && item.ItemSO == itemSO);
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
            if (_itemsCount == _settings.itemsCount - 1) { // Inventory full
                //NotificationManager.Instance.RaiseNotification(new Notification("Inventory is full", 3f));
                if (_selectedSlot == 0) {
                    _selectedSlot = 1;
                    SelectedSlotChanged?.Invoke(this, new SelectedSlotChangedEventArgs() {
                        Slot = _selectedSlot
                    });
                }
                RemoveItem(out Item i);
                ShowItem(i);
            }

            if (_items[_selectedSlot] is null && _selectedSlot != 0) { // Put item in the selected slot
                _items[_selectedSlot] = item;
                _itemsCount++;
                ItemInserted?.Invoke(this, new ItemInsertedEventArgs() {
                    Slot = _selectedSlot,
                    Item = item
                });
            }
            else { // Put item into first empty slot
                for (int i = 1; i < _settings.itemsCount; i++) {
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
            item = HideItem(item);

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
            if (item.Durability <= 0) {
                RemoveItem(out var removedItem);
                Destroy(item.gameObject);
            }
            return true;
        }

        /// <summary>
        /// Retrieve and remove item from the Inventory. Empty slot is always the result.
        /// </summary>
        /// <param name="item">Removed item if selected slot was not empty.</param>
        /// <returns>`True` if item was removed, `false` if slot was empty. If `false`, retrieved item will be null.</returns>
        public bool RemoveItem(out Item item) {
            if (_selectedSlot == 0) {
                item = null;
                return false;
            }
            item = _items[_selectedSlot];
            _items[_selectedSlot] = null;
            ItemRemoved?.Invoke(this, new ItemRemovedEventArgs() {
                Slot = _selectedSlot,
                Item = item
            });
            _itemsCount--;
            return item is not null;
        }

        /// <summary>
        /// Removes item based on provided ItemSO. If there are multiple items with the same ItemSO, only the first one will be removed.
        /// </summary>
        /// <param name="itemSO">Definition of the item to be removed.</param>
        /// <returns></returns>
        public bool RemoveItem(ItemSO itemSO) {
            var item = _items.FirstOrDefault(item => item is not null && item.ItemSO == itemSO);
            var index = Array.IndexOf(_items, item);
            if (index == -1 || index == 0) {
                return false;
            }
            item = _items[index];
            _items[index] = null;
            _selectedSlot = index;
            SelectedSlotChanged?.Invoke(this, new SelectedSlotChangedEventArgs() {
                Slot = _selectedSlot
            });
            ItemRemoved?.Invoke(this, new ItemRemovedEventArgs() {
                Slot = _selectedSlot,
                Item = item
            });
            _itemsCount--;
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

        /// <summary>
        /// Function to drop an item from inventory.
        /// After the drop, item is spawned inside the "Items" object of the current time. If the object "Items" doesn't exist,
        /// it is spawned inside the "Content" object of the current time.
        /// THIS FUNCTION assumes, that the "Content" object exist. If it doesn't, object is spawed on the "Game" scene.
        /// </summary>
        private void OnDropItemClicked(InputAction.CallbackContext ctx) {
            if (_items[_selectedSlot] == null) return;
            RemoveItem(out Item item);
            ShowItem(item);
            //Instantiate(item.ItemSO.prefab, FindObjectOfType<Player>().gameObject.transform.position, Quaternion.identity, FindTransformOfCurrentTime());
        }

        private Transform FindTransformOfCurrentTime() {
            Transform currentTimeTransform = this.transform;
            Player player = FindObjectOfType<Player>();
            TimelineMaps timelineMaps = FindObjectOfType<LevelsManager>().CurrentLevelManager.FindTimelineMaps();
            switch (player.GetComponent<TimeChanger>().actualTime) {
                case LevelTimeChange.TimeLine.Past:
                    currentTimeTransform = timelineMaps.past;
                    break;
                case LevelTimeChange.TimeLine.Present:
                    currentTimeTransform = timelineMaps.present;
                    break;
                case LevelTimeChange.TimeLine.Future:
                    currentTimeTransform = timelineMaps.future;
                    break;
            }
            Transform transformOfContent = currentTimeTransform.transform.Find("Content")?.transform;
            Transform transformOfItems = transformOfContent.transform.Find("Items")?.transform;
            if (transformOfItems) {
                currentTimeTransform = transformOfItems;
            }
            else {
                GameObject emptyGameObject = new GameObject("Items");
                emptyGameObject.transform.parent = transformOfContent;
                currentTimeTransform = emptyGameObject.transform;
            }
            return currentTimeTransform;
        }

        private Item HideItem(Item item) {
            if (item.transform.parent == null) {
                var itemUniqueId = item.ID;
                item = Instantiate(item);
                item.ID = itemUniqueId;
            }
            item.transform.SetParent(itemHideout);
            item.transform.position = new Vector3(0f, 0f, 0f);
            item.GetComponent<SpriteRenderer>().enabled = false;
            item.GetComponent<CircleCollider2D>().enabled = false;
            return item;
        }

        private void ShowItem(Item itemToFind) {
            foreach (Transform child in itemHideout) {
                Item item = child.gameObject.GetComponent<Item>();
                Debug.Log(item.Equals(itemToFind));
                if (item.Equals(itemToFind)) {
                    item.transform.parent = FindTransformOfCurrentTime();
                    item.transform.position = FindObjectOfType<Player>().gameObject.transform.position;
                    item.GetComponent<SpriteRenderer>().enabled = true;
                    item.GetComponent<CircleCollider2D>().enabled = true;
                    return;
                }
            }
        }
    }
}