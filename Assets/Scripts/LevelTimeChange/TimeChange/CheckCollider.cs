using UnityEngine;
using CoinPackage.Debugging;

namespace LevelTimeChange.TimeChange {
    /// <summary>
    /// Provides checking collision for time change mechanic.
    /// </summary>
    public class CheckCollider : MonoBehaviour {
        private CircleCollider2D _collider;
        private bool _isTouching;

        private void Awake() {
            _collider = GetComponent<CircleCollider2D>();
            _collider.isTrigger = true;
            _collider.offset = new Vector2(0, 0f);
            _collider.radius = 0.3f;
        }

        /// <summary>
        /// Checking if Player can change time. CheckCollider's collider isn't touching any not trigger collider.
        /// </summary>
        /// <returns>Whether Player can change time or not</returns>
        public bool IsNotTouching() {
            return !_isTouching;
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (!other.isTrigger) _isTouching = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.isTrigger) _isTouching = false;
        }
    }
}