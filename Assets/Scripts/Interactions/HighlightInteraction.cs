using System;
using Interactions.Interfaces;
using UnityEngine;

namespace Interactions {
    public class HighlightInteraction : MonoBehaviour, IHighlightable {

        private SpriteRenderer _sprite;
        private Material _material;
        private void Awake() {
            _sprite = GetComponent<SpriteRenderer>();
            _material = _sprite.material;
            _material.SetTexture("_texture",_sprite.sprite.texture);
            _material.SetFloat("_scale",0.01f);
        }

        public void EnableHighlight() {
            _material.SetColor("_color",Color.green);
        }

        public void EnableFocusedHighlight() {
            _material.SetColor("_color",Color.red);
        }

        public void DisableHighlight() {
            _material.SetColor("_color",Color.white);
        }
    }
}