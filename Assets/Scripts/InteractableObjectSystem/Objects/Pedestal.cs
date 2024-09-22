using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using DataPersistence.DataTypes;
using Interactions;
using InventorySystem;
using Items;
using NPC;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class Pedestal : PersistentInteractableObject {
        [SerializeField] private List<ItemSO> _interactedWith = new();
        [SerializeField] private ItemSO _goodRock;
        [SerializeField] private SpriteRenderer _rockHolder;
        [SerializeField] private LockedDoor _lockedDoor;
        [SerializeField] private ItemSO _defaultRock;

        private ItemSO _currentRock;
        private bool _loaded = false;
        private void Start() {
            if (!_loaded) {
                SetUpNewRock(_defaultRock);
                _currentRock = _defaultRock;
            }

            LeanTween.moveY(_rockHolder.gameObject, _rockHolder.transform.position.y + 0.3f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        }

        public override bool InteractionItem(Item item) {
            if (_interactedWith.Contains(item.ItemSO)) {
                SetUpNewRock(item.ItemSO);
                GameObject tempItem = Instantiate(_currentRock.prefab);
                tempItem.transform.SetParent(transform);
                _currentRock = item.ItemSO;
                item.Hide();
                Inventory.Instance.RemoveItem(item.ItemSO);
                Destroy(item.gameObject);
                Inventory.Instance.InsertItem(tempItem.GetComponent<Item>());
            }

            return false;
        }

        public override void LoadPersistentData(GameData gameData) {
            if (!gameData.ContainsObjectData(ID))
                return;

            _loaded = true;

            var pedestalData = gameData.GetObjectData<PedestalData>(ID);
            _currentRock = pedestalData.data.currentRock;
            SetUpNewRock(_currentRock);
        }

        public override void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var pedestalData = gameData.GetObjectData<PedestalData>(ID);
                pedestalData.data.currentRock = _currentRock;
                pedestalData.SerializeInheritance();
                gameData.SetObjectData(pedestalData);
            }
            else {
                var pedestalData = new PedestalData {
                    data = new PedestalData.PedestalSubData {
                        currentRock = _currentRock
                    },
                    id = ID
                };
                pedestalData.SerializeInheritance();
                gameData.SetObjectData(pedestalData);
            }
        }

        private void SetUpNewRock(ItemSO itemSO) {
            _rockHolder.sprite = itemSO.image;
            if (itemSO == _goodRock) {
                ToogleDoorsOpening(true);
            }
            else {
                ToogleDoorsOpening(false);
            }
        }

        private void ToogleDoorsOpening(bool state) {
            if (state) {
                _lockedDoor.OpenDoor();
            }
            else {
                _lockedDoor.CloseDoor();
                _lockedDoor.LockDoor();
            }
        }
    }
}