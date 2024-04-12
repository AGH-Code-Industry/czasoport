using System;
using Interactions.Interfaces;
using UnityEngine;
using System.Collections;

namespace Interactions {
    public class HighlightInteraction : MonoBehaviour, IHighlightable {
        public float scale = 0.02f;
        private SpriteRenderer _sprite;
        private Material _material;

        private bool _isFocused = false;
        private IEnumerator _fadeIn;
        private IEnumerator _fadeOut;

        private float _animationTime = 0.3f;
        private float _animationTimer;


        private void Start() {
            _sprite = GetComponent<SpriteRenderer>();
            _material = _sprite.material;
            _material.SetTexture("_texture", _sprite.sprite.texture);
            _material.SetFloat("_OnOff", 0f);
            _material.SetFloat("_scale", scale);
            _material.SetColor("_color", Color.red);
            _fadeIn = TurnHighlight(0f, 1f);
            _fadeOut = TurnHighlight(1f, 0f);
        }

        public void EnableHighlight() { }

        public void DisableHighlight() {
            if (_isFocused) {
                StopCoroutine(_fadeIn);
                _fadeOut = TurnHighlight(1f, 0f);
                StartCoroutine(_fadeOut);
                _fadeIn = TurnHighlight(0f, 1f);
            }
        }

        public void EnableFocusedHighlight() {
            if (!_isFocused) {
                StopCoroutine(_fadeOut);
                _fadeOut = TurnHighlight(1f, 0f);
                StartCoroutine(_fadeIn);
                _fadeOut = TurnHighlight(1f, 0f);
            }
        }

        public void DisableFocusedHighlight() {
            if (_isFocused) {
                StopCoroutine(_fadeIn);
                _fadeOut = TurnHighlight(1f, 0f);
                StartCoroutine(_fadeOut);
                _fadeIn = TurnHighlight(0f, 1f);
            }
        }

        private IEnumerator TurnHighlight(float startAlpha, float targetAlpha) {
            if (targetAlpha == 1f) _isFocused = true;
            else _isFocused = false;

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