using System;
using System.Linq;
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
        public bool Hidden { get; set; } = false;
        public bool BlockDestroying { get; set; } = false;

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

            //LeanTween.scale(this.gameObject, new Vector3(1.2f, 1.2f, 1f), 0.4f).setLoopPingPong();
            if (Durability == 0) Durability = ItemSO.durability;
        }

        public void LoadPersistentData(GameData gameData) {
            if (!gameData.IsLevelSaved(LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId))
                return;

            if (gameData.playerGameData.inventory.Any(it => it.id.Equals(ID))) {
                if (!BlockDestroying && !Hidden)
                    Destroy(gameObject);
                return;
            }

            if (gameData.ContainsObjectData(ID)) {
                var itemData = gameData.GetObjectData<ItemData>(ID);

                if (itemData.data.mapId != LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId)
                    return;

                Durability = itemData.data.durability;
                itemSO = itemData.data.itemSo;
                transform.position = itemData.data.position;
                Hidden = itemData.data.hidden;
                if (Hidden)
                    Hide();
            }
        }

        public void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var itemData = gameData.GetObjectData<ItemData>(ID);
                itemData.data.durability = Durability;
                itemData.data.itemSo = ItemSO;
                itemData.data.position = transform.position;
                itemData.data.mapId = LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId;
                itemData.data.hidden = Hidden;
                itemData.SerializeInheritance();
                gameData.SetObjectData(itemData);
            }
            else {
                var itemData = new ItemData {
                    data = new ItemData.ItemSubData {
                        durability = Durability,
                        itemSo = ItemSO,
                        position = transform.position,
                        mapId = LevelsManager.Instance.CurrentLevelManager.currentLevel.uniqueId,
                        hidden = Hidden
                    },
                    id = ID
                };
                itemData.SerializeInheritance();
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

        public void Hide() {
            Hidden = true;
            transform.SetParent(Inventory.Instance.itemHideout);
            //transform.position = new Vector3(0f, 0f, 0f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
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