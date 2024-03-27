using System.Collections;
using System.Collections.Generic;
using Interactions;
using InventorySystem;
using Items;
using NPC;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class Pedestal : InteractableObject {
        [SerializeField] private List<ItemSO> _interactedWith = new();
        [SerializeField] private ItemSO _goodRock;
        [SerializeField] private SpriteRenderer _rockHolder;
        [SerializeField] private LockedDoor _lockedDoor;
        [SerializeField] private ItemSO _defaultRock;

        private ItemSO _currentRock;
        private void Start() {
            SetUpNewRock(_defaultRock);
            _currentRock = _defaultRock;
            LeanTween.moveY(_rockHolder.gameObject, _rockHolder.transform.position.y + 0.3f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        }

        public override bool InteractionItem(Item item) {
            if (_interactedWith.Contains(item.ItemSO)) {
                SetUpNewRock(item.ItemSO);
                GameObject tempItem = Instantiate(_currentRock.prefab);
                tempItem.transform.parent = transform;
                Inventory.Instance.InsertItem(tempItem.GetComponent<Item>());
                _currentRock = item.ItemSO;
                Destroy(item.gameObject);
                return true;
            }

            return false;
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
            }
        }
    }
}