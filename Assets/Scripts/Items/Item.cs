using System;
using UnityEngine;
using Interactions.Interfaces;
using CustomInput;

namespace Items
{
    public class Item : MonoBehaviour, IInteractableHand, ILongInteractableHand
    {
        [SerializeField] private ItemSO itemSO;
        [SerializeField] private Color outlineColor;
        [SerializeField] private float border = 1f;
        private SpriteRenderer _spriteRenderer;
        private int _toogleOutline = 0;
        private Color _black = new Color(0, 0, 0, 0);
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.material.SetTexture("_MainTex",itemSO.texture);
            _spriteRenderer.material.SetColor("_color",_black);
            _spriteRenderer.material.SetFloat("_thickness",border);
            _spriteRenderer.material.SetFloat("_alpha",0f);
            _spriteRenderer.sprite = itemSO.image;
        }

        private void OnEnable()
        {
            CInput.InputActions.Teleport.TeleportBack.performed += ctx => {ToogleHighlight();};
        }

        private void OnDisable() {
            CInput.InputActions.Teleport.TeleportBack.performed -= ctx => {ToogleHighlight();};
        }
        
        public void ToogleHighlight()
        {
            _toogleOutline = (_toogleOutline + 1) % 2;
            if (_toogleOutline == 0)
            {
                _spriteRenderer.material.SetColor("_color",_black);
                _spriteRenderer.material.SetFloat("_alpha",0f);
            }
            else
            {
                _spriteRenderer.material.SetColor("_color",outlineColor);
                _spriteRenderer.material.SetFloat("_alpha",1f);
            }
        }

        public ItemSO GetItemSO() {
            return itemSO;
        }

        public void GetItem() {
            PlayerInteract.Instance.SetHoldingItem(this);
        }

        public void InteractionHand() {
            throw new NotImplementedException();
        }

        public void LongInteractionHand() {
            throw new NotImplementedException();
        }
    }
}