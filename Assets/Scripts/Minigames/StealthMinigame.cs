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
        private bool _duringMinigame = true;

        private bool _playerInArea {
            get {
                return _area.IsInArea;
            }
        }
        private void Start() {
            StartMinigame();
        }

        private void Update() {
            if (_targetInHand & _playerInArea) EndMinigame();
        }

        public void RestartMinigame() {
            if (!_duringMinigame) return;
            if (_targetInHand) ResetTarget();
            if (_playerInArea) StartCoroutine(SetPlayerToStart());
        }

        private IEnumerator SetPlayerToStart() {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(_animationTime/2);
            Player.Instance.transform.position = startPoint.position;
            yield return new WaitForSeconds(_animationTime/2);
            animator.SetTrigger("End");
        }

        private void IsItemTarget(object sender, ItemInsertedEventArgs args) {
            if (args.Item.ItemSO == _itemToSteal) _targetInHand = true;
        }
        
        private void ResetTarget() {
            _targetInHand = false;
            Inventory.Instance.RemoveItem(_itemToSteal);
            toSteal.transform.position = _targetPosition;
        }
        
        public void StartMinigame() {
            _duringMinigame = true;
            _itemToSteal = toSteal.GetComponent<Item>().ItemSO;
            _targetPosition = toSteal.transform.position;
            animator = TimeChanger.Instance.Animator;
            _animationTime = DeveloperSettings.Instance.tpcSettings.timelineChangeAnimLength;
            Inventory.Instance.ItemInserted += IsItemTarget;
        }

        public void EndMinigame() {
            if (!_duringMinigame) return;
            _duringMinigame = false;
            Inventory.Instance.ItemInserted -= IsItemTarget;
        }
    }
}