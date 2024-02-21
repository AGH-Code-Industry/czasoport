using System;
using Interactions.Interfaces;
using UnityEngine;
using System.Collections;

namespace Interactions {
    public class HighlightInteraction : MonoBehaviour, IHighlightable {

        private SpriteRenderer _sprite;
        private Material _material;
        private bool _isFocused = false;
        private float _animationTime = 0.3f;
        private float _animationTimer;

        private void Start() {
            _sprite = GetComponent<SpriteRenderer>();
            _material = _sprite.material;
            _material.SetTexture("_texture", _sprite.sprite.texture);
            _material.SetFloat("_OnOff",0f);
            _material.SetFloat("_scale",0.01f);
			_material.SetColor("_color",Color.green);
        }

        public void EnableHighlight() {
            if (_isFocused) StartCoroutine(ToogleHighlight());
        }

        public void EnableFocusedHighlight() {
            if (!_isFocused) StartCoroutine(ToogleHighlight());
        }

        public void DisableHighlight() {
            if (_isFocused) StartCoroutine(ToogleHighlight());
        }
        
        private IEnumerator ToogleHighlight() {
			_isFocused = !_isFocused;
            float targetAlpha = 1f - _material.GetFloat("_OnOff");
			float startAlpha = _material.GetFloat("_OnOff");;
            _animationTimer = _animationTime;
                
            while (_animationTimer > 0f) {
            	_animationTimer -= Time.deltaTime;
                yield return null;
                _material.SetFloat("_OnOff",
                	Mathf.Lerp(startAlpha, targetAlpha,
					(_animationTime - _animationTimer) / _animationTime));
            }
            _material.SetFloat("_OnOff", targetAlpha);
        }
    }
}