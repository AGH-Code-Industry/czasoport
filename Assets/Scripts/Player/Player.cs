using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        public static Player Instance = null;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }
    }
}