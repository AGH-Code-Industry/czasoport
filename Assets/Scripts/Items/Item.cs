using System;
using Application;
using CoinPackage.Debugging;
using Interactions;
using UnityEngine;
using Interactions.Interfaces;
using InventorySystem;

namespace Items
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class Item : MonoBehaviour, IHandInteractable, ILongHandInteractable
    {
        [SerializeField] private ItemSO itemSO;

        public ItemSO ItemSO => itemSO;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.ITEMS];

        private void Awake() {
            // GetComponent<CircleCollider2D>().isTrigger = true;
            if (gameObject.layer != LayerMask.NameToLayer("Interactables")) {
                _logger.LogWarning(
                    $"Item {this} {"is not" % Colorize.Red} in {"Interactables" % Colorize.Green} layer.",
                    this
                    );
            }
        }

        public void InteractionHand() {
            _logger.Log($"Item {this} is being {"short interacted" % Colorize.Green} with.", this);
        }

        public void LongInteractionHand() {
            _logger.Log($"Item {this} is being {"long interacted" % Colorize.Cyan} with.", this);
        }
        
        

        public override string ToString() {
            return $"[Item, {itemSO.name}]" % Colorize.Purple;
        }
    }
}