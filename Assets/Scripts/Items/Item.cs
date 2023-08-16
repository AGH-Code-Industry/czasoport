using System;
using UnityEngine;
using Interactions.Interfaces;

namespace Items
{
    public class Item : MonoBehaviour, IInteractableHand, ILongInteractableHand
    {
        [SerializeField] private ItemSO _itemSO;
        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _itemSO.image;
        }

        public void ToogleHighlight() {
            throw new NotImplementedException();
        }
    
        public void InteractionHand() {
            throw new NotImplementedException();
        }
    
        public void LongInteractionHand() {
            throw new NotImplementedException();
        }
    }
}