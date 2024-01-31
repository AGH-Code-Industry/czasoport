using System;
using Interactions.Interfaces;
using UnityEngine;

namespace Interactions {
    public class HighlightInteraction : MonoBehaviour, IHighlightable {

        private SpriteRenderer _sprite;
        private Material _material;

        private void Start() {
            _sprite = GetComponent<SpriteRenderer>();
            _material = _sprite.material;
            _material.SetTexture("_texture", _sprite.sprite.texture);
            _material.SetFloat("_OnOff",0f);
            _material.SetFloat("_scale",0.01f);
        }

        public void EnableHighlight() {
            _material.SetFloat("_OnOff",1f);
            _material.SetColor("_color",Color.green);
        }

        public void EnableFocusedHighlight() {
            _material.SetFloat("_OnOff",1f);
            _material.SetColor("_color", Color.red);
        }

        public void DisableHighlight() {
            _material.SetFloat("_OnOff",0f);
            _material.SetColor("_color",Color.white);
        }
    }
}