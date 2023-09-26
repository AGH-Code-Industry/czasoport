using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Items;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    public class Stone : InteractableObject {
        private enum StoneState {
            Relaxed,
            Destroyed
        }

        [SerializeField] private Sprite destroyedRock;
        [SerializeField] private List<int> canDestroy = new();

        private CircleCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private StoneState _state;

        private void Awake() {
            _state = StoneState.Relaxed;
            _collider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override bool InteractionItem(Item item) {
            if (_state == StoneState.Destroyed) return false;
            if (canDestroy.Contains(item.ItemSO.id)) {
                Destroy();
                return true;
            }
            CDebug.Log("Stone is too awesome for you");
            return false;
        }
        
        private void Destroy() {
            _state = StoneState.Destroyed;
            _collider.isTrigger = true;
            _spriteRenderer.sprite = destroyedRock;
            CDebug.Log("Stone have been broken");
        }
    }
}