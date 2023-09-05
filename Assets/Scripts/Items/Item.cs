using System;
using UnityEngine;
using Interactions.Interfaces;
using CustomInput;

namespace Items
{
    public class Item : MonoBehaviour, IInteractableHand, ILongInteractableHand
    {
        [SerializeField] private ItemSO itemSO;
        private SpriteRenderer _spriteRenderer;
        private int _toogleOutline = 0;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = itemSO.image;
            _spriteRenderer.material.SetTexture("_MainTex",_spriteRenderer.sprite.texture);
            _spriteRenderer.material.SetFloat("_alpha",0f);
        }

        private void OnEnable()
        {
            CInput.InputActions.Teleport.TeleportBack.performed += ctx => {ToogleHighlight();};
        }

        private void OnDisable() {
            CInput.InputActions.Teleport.TeleportBack.performed -= ctx => {ToogleHighlight();};
        }
        
        public void ToogleHighlight() {
            _toogleOutline = (_toogleOutline + 1) % 2;
            _spriteRenderer.material.SetFloat("_alpha",_toogleOutline);
        }

        public ItemSO GetItemSO() {
            return itemSO;
        }

        public void InteractionHand() {
            throw new NotImplementedException();
        }

        public void LongInteractionHand() {
            throw new NotImplementedException();
        }
    }
}