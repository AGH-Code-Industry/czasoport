using System;
using System.Xml;
using Application;
using CoinPackage.Debugging;
using Interactions;
using UnityEngine;
using Interactions.Interfaces;
using InventorySystem;
using Utils.Attributes;

namespace Items
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class Item : MonoBehaviour, IHandInteractable, ILongHandInteractable {
        [ObjectIdentifier] public string uniqueId;
        [SerializeField] private ItemSO itemSO;

        public ItemSO ItemSO => itemSO;
        public int Durability { get; set; }

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.ITEMS];
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _collider;

        public Item() : base() {}
        
        public Item(ItemSO itemScriptableObject) : base() {
            itemSO = itemScriptableObject;
        }
        
        private void Awake() {
            uniqueId = Guid.NewGuid().ToString("N");
            if (gameObject.layer != LayerMask.NameToLayer("Items")) {
                _logger.LogWarning(
                    $"Item {this} {"is not" % Colorize.Red} in {"Items" % Colorize.Green} layer.",
                    this
                    );
            }

            Durability = ItemSO.durability;
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

        public override bool Equals(object other) {
            var item = other as Item;
            return this.uniqueId.Equals(item.uniqueId);
        }
    }
}