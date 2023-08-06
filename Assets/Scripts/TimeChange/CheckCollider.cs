using UnityEngine;
using CoinPackage.Debugging;

namespace TimeChange
{
    /// <summary>
    /// Provides checking collision for time change mechanic.
    /// </summary>
    public class CheckCollider : MonoBehaviour
    {
        private BoxCollider2D _box;
        private bool _isTouching = false;

        private void Awake() {
            _box = GetComponent<BoxCollider2D>();
            _box.isTrigger = true;
        }

        /// <summary>
        /// Checking if Player can change time. CheckCollider's collider isn't touching any not trigger collider.
        /// </summary>
        /// <returns>Bool</returns>
        public bool IsNotTouching() {
            return !_isTouching;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.isTrigger) _isTouching = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.isTrigger) _isTouching = false;
        }
    }
}
