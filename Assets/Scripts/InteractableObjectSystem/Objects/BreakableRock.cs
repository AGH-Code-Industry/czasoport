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
        private AudioSource _audioSource;

        private void Awake() {
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
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
            _particleSystem.Play();
            Invoke("HideSprite", 0.5f);
            CDebug.Log("Broken");
            _audioSource.enabled = true;
            _audioSource.Play();
            transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }

        private void HideSprite() {
            _collider.enabled = false;
            _renderer.sprite = null;
        }
    }
}