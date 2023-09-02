using System;
using Interactions.Interfaces;
using UnityEngine;

namespace Interactions {
    public class HighlightInteraction : MonoBehaviour, IHighlightable {

        private SpriteRenderer _sprite;
        private void Awake() {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void EnableHighlight() {
            _sprite.color = Color.cyan;
        }

        public void EnableFocusedHighlight() {
            _sprite.color = Color.blue;
        }

        public void DisableHighlight() {
            _sprite.color = Color.red;
        }
    }
}