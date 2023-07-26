using UnityEngine;
using CoinPackage.Debugging;

namespace TimeChange
{
    public class CheckCollider : MonoBehaviour
    {
        private BoxCollider2D _box;
        private bool _isTouching = false;

        private void Awake() {
            _box = GetComponent<BoxCollider2D>();
            _box.isTrigger = true;
        }

        public bool isNotTouching() {
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
