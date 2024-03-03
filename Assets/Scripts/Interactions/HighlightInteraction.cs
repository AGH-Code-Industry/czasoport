using System;
using Interactions.Interfaces;
using UnityEngine;
using System.Collections;

namespace Interactions {
    public class HighlightInteraction : MonoBehaviour, IHighlightable {

        private SpriteRenderer _sprite;
        private Material _material;
        private bool _isFocused = false;
        private bool _stopAnimation = false;
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
        
        public void EnableHighlight() {}
        public void DisableHighlight() {
            if (_isFocused) {
                _stopAnimation = true;
                StartCoroutine(TurnHighlight(1f, 0f));
            }
        }

        public void EnableFocusedHighlight() {
            if (!_isFocused) {
                _stopAnimation = true;
                StartCoroutine(TurnHighlight(0f, 1f));
            }
        }

        public void DisableFocusedHighlight() {
            if (_isFocused) {
                _stopAnimation = true;
                StartCoroutine(TurnHighlight(1f, 0f));
            }
        }
        
        private IEnumerator TurnHighlight(float startAlpha,float targetAlpha) {
            _stopAnimation = false;
            if (targetAlpha == 1f) _isFocused = true;
            else _isFocused = false;
            
            _animationTimer = _animationTime;
                
            while (_animationTimer > 0f) {
                if (_stopAnimation) break;
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
