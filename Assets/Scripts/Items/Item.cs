using System;
using System.Xml;
using Application;
using CoinPackage.Debugging;
using DataPersistence;
using DataPersistence.DataTypes;
using Interactions;
using UnityEngine;
using Interactions.Interfaces;
using InventorySystem;
using LevelTimeChange.LevelsLoader;
using Utils;
using Utils.Attributes;

namespace Items {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class Item : MonoBehaviour, IHandInteractable, ILongHandInteractable, IPersistentObject {
        [SerializeField] private ItemSO itemSO;
        [field: SerializeField] public SerializableGuid ID { get; set; }

        public bool SceneObject { get; } = true;

        public ItemSO ItemSO => itemSO;
        private int _durability = 0;
        public int Durability {
            get { return _durability; }
            set { _durability = value; }
        }

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.ITEMS];
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _collider;

        public Item() : base() { }

        public Item(ItemSO itemScriptableObject) : base() {
            itemSO = itemScriptableObject;
        }

        private void Awake() {
            if (gameObject.layer != LayerMask.NameToLayer("Items")) {
                _logger.LogWarning(
                    $"Item {this} {"is not" % Colorize.Red} in {"Items" % Colorize.Green} layer.",
                    this
                    );
            }

            LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1f), 0.4f).setLoopPingPong();
            if (Durability == 0) Durability = ItemSO.durability;
        }

        public void LoadPersistentData(GameData gameData) {
            if (!gameData.IsLevelSaved(LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId))
                return;

            if (gameData.ContainsObjectData(ID)) {
                var itemData = gameData.GetObjectData<ItemData>(ID);

                if (itemData.mapId != LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId)
                    return;

                var newItem = Instantiate(ItemSO.prefab, itemData.position, Quaternion.identity).GetComponent<Item>();
                newItem.Durability = itemData.durability;
                newItem.ID = ID;
            }

            Destroy(gameObject);
        }

        public void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var itemData = gameData.GetObjectData<ItemData>(ID);
                itemData.durability = Durability;
                itemData.itemSo = ItemSO;
                itemData.position = transform.position;
                itemData.mapId = LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId;
            }
            else {
                var itemData = new ItemData {
                    durability = Durability,
                    itemSo = ItemSO,
                    position = transform.position,
                    mapId = LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId
                };
                gameData.SetObjectData(itemData);
            }
        }

        public void InteractionHand() {
            _logger.Log($"Item {this} is being {"short interacted" % Colorize.Green} with.", this);
            Inventory.Instance.InsertItem(this);
        }

        public void LongInteractionHand() {
            _logger.Log($"Item {this} is being {"long interacted" % Colorize.Cyan} with.", this);
        }

        public override string ToString() {
            return itemSO.ToString();
        }

        protected bool Equals(Item other) {
            return base.Equals(other) && ID.Equals(other.ID);
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode(), ID);
        }
    }
}