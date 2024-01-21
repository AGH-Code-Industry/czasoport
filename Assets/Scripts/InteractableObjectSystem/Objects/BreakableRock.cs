using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    public class BreakableRock : InteractableObject {
        
        private enum RockState {
            NotDestroyed,
            Destroyed
        }
        
        [SerializeField] private List<ItemSO> _interactedWith;
        [SerializeField] private ParticleSystem _particleSystem;
        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;
        private RockState _state;

        private void Awake() {
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _state = RockState.NotDestroyed;
        }

        public override void InteractionHand() {
            if (_state == RockState.NotDestroyed) {
                NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
                return;
            }
        }

        public override bool InteractionItem(Item item) {
            if (_state != RockState.NotDestroyed) {
                return false;
            }
            if (_interactedWith.Contains(item.ItemSO)) {
                Break();
                NotificationManager.Instance.RaiseNotification(definition.successfulItemInterNotification);
                return true;
            }
            NotificationManager.Instance.RaiseNotification(definition.failedItemInterNotification);
            return false;
        }

        private void Break() {
            _state = RockState.Destroyed;
            _collider.enabled = false;
            _particleSystem.Play();
            Invoke("HideSprite",1.5f);
            CDebug.Log("Broken");
        }

        private void HideSprite() {
            _renderer.sprite = null;
        }
    }
}
