using System;
using System.Collections;
using DataPersistence;
using DataPersistence.DataTypes;
using InteractableObjectSystem;
using InventorySystem;
using InventorySystem.EventArguments;
using Items;
using LevelTimeChange.TimeChange;
using PlayerScripts;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Minigames {
    public class StealthMinigame : MonoBehaviour, IPersistentObject {
        [field: SerializeField] public SerializableGuid ID { get; set; }
        public bool SceneObject { get; }
        [field: SerializeField] public SerializableGuid toStealId { get; set; }
        private GameObject _toSteal;

        private ItemSO _itemToSteal;
        private Item _item = null;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private CheckPlayerInArea _area;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Animator animator;

        private float _animationTime;
        public bool TargetInHand = false;
        private bool _duringRestartingPos = false;
        private bool _playerInArea {
            get {
                return _area.IsInArea;
            }
        }

        private void Start() {
            FindTarget();
            Debug.Log(_toSteal);

            animator = GameObject.FindGameObjectWithTag("TimeChangeAnimation").GetComponent<Animator>();
            StartMinigame();
        }

        private void FindTarget() {
            foreach (Transform child in transform) {
                if (child.TryGetComponent(out _item)) {
                    if (_item.ID.Equals(ID)) break;
                }
                else _item = null;
            }

            if (_item) {
                _toSteal = _item.gameObject;
                return;
            }

            foreach (Transform child in Inventory.Instance.itemHideout) {
                if (child.TryGetComponent(out _item)) {
                    if (_item.ID.Equals(toStealId)) break;
                }
                else _item = null;
            }

            if (_item) {
                _toSteal = _item.gameObject;
                _itemToSteal = _item.ItemSO;
                if (Inventory.Instance.ContainsItem(_itemToSteal))
                    TargetInHand = true;
            }
        }

        public void RestartMinigame() {
            FindTarget();
            Debug.Log(_toSteal);
            if (!_playerInArea) return;
            if (TargetInHand) ResetTarget();
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
            if (_itemToSteal == null) return;
            if (args.Item.ItemSO == _itemToSteal & Inventory.Instance.itemsCount < DeveloperSettings.Instance.invSettings.itemsCount) TargetInHand = true;
        }

        private void IsItemTargetRemove(object sender, ItemRemovedEventArgs args) {
            if (_itemToSteal == null) return;
            if (args.Item.ItemSO == _itemToSteal) TargetInHand = false;
        }

        private void ResetTarget() {
            TargetInHand = false;
            Inventory.Instance.RemoveItem(_itemToSteal);
            _toSteal.transform.parent = transform;
            _toSteal.transform.localPosition = targetPosition;
            _toSteal.GetComponent<SpriteRenderer>().enabled = true;
            _toSteal.GetComponent<CircleCollider2D>().enabled = true;
        }

        public void StartMinigame() {
            if (_itemToSteal == null) return;
            _animationTime = DeveloperSettings.Instance.tpcSettings.timelineChangeAnimLength;
            Inventory.Instance.ItemInserted += IsItemTargetInsert;
            Inventory.Instance.ItemRemoved += IsItemTargetRemove;
        }

        public void OnDisable() {
            Inventory.Instance.ItemInserted -= IsItemTargetInsert;
            Inventory.Instance.ItemRemoved -= IsItemTargetRemove;
        }

        public void LoadPersistentData(GameData gameData) {
            if (!gameData.ContainsObjectData(ID))
                return;

            var minigameData = gameData.GetObjectData<InteractableData>(ID);
            TargetInHand = minigameData.data.state == 1;
        }

        public void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var minigameData = gameData.GetObjectData<InteractableData>(ID);
                minigameData.data.state = TargetInHand ? 1 : 0;
                minigameData.SerializeInheritance();
                gameData.SetObjectData(minigameData);
            }
            else {
                var minigameData = new InteractableData {
                    data = new InteractableData.InteractableSubData {
                        state = TargetInHand ? 1 : 0
                    },
                    id = ID
                };
                minigameData.SerializeInheritance();
                gameData.SetObjectData(minigameData);
            }
        }
    }
}