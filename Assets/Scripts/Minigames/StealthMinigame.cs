using System;
using System.Collections;
using InventorySystem;
using InventorySystem.EventArguments;
using Items;
using LevelTimeChange.TimeChange;
using PlayerScripts;
using Settings;
using UnityEngine;

namespace Minigames {
    public class StealthMinigame : MonoBehaviour {
        [SerializeField] private GameObject toSteal;
        private ItemSO _itemToSteal;
        private Vector3 _targetPosition;
        [SerializeField] private CheckPlayerInArea _area;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Animator animator;

        private float _animationTime;
        private bool _targetInHand = false;
        private bool _duringRestartingPos = false;
        private bool _playerInArea {
            get {
                return _area.IsInArea;
            }
        }

        private void Start() {
            StartMinigame();
        }

        public void RestartMinigame() {
            if (!_playerInArea) return;
            if (_targetInHand) ResetTarget();
            if (!_duringRestartingPos) StartCoroutine(SetPlayerToStart());
        }

        private IEnumerator SetPlayerToStart() {
            _duringRestartingPos = true;
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(_animationTime / 2);
            Player.Instance.transform.position = startPoint.position;
            yield return new WaitForSeconds(_animationTime / 2);
            animator.SetTrigger("End");
            _duringRestartingPos = false;
        }

        private void IsItemTargetInsert(object sender, ItemInsertedEventArgs args) {
            if (args.Item.ItemSO == _itemToSteal & Inventory.Instance.itemsCount < DeveloperSettings.Instance.invSettings.itemsCount) _targetInHand = true;
        }

        private void IsItemTargetRemove(object sender, ItemRemovedEventArgs args) {
            if (args.Item.ItemSO == _itemToSteal) _targetInHand = false;
        }

        private void ResetTarget() {
            _targetInHand = false;
            Inventory.Instance.RemoveItem(_itemToSteal);
            toSteal.transform.parent = transform;
            toSteal.transform.position = _targetPosition;
            toSteal.GetComponent<SpriteRenderer>().enabled = true;
            toSteal.GetComponent<CircleCollider2D>().enabled = true;
        }

        public void StartMinigame() {
            _itemToSteal = toSteal.GetComponent<Item>().ItemSO;
            _targetPosition = toSteal.transform.position;
            animator = TimeChanger.Instance.Animator;
            _animationTime = DeveloperSettings.Instance.tpcSettings.timelineChangeAnimLength;
            Inventory.Instance.ItemInserted += IsItemTargetInsert;
            Inventory.Instance.ItemRemoved += IsItemTargetRemove;
        }

        public void OnDisable() {
            Inventory.Instance.ItemInserted -= IsItemTargetInsert;
            Inventory.Instance.ItemRemoved -= IsItemTargetRemove;
        }
    }
}