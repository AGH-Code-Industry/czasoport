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
            _sprite.color = Color.green;
        }

        public void EnableFocusedHighlight() {
            _sprite.color = Color.red;
        }

        public void DisableHighlight() {
            _sprite.color = Color.white;
        }
    }
}