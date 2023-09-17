using UnityEngine;
using CustomInput;

namespace PlayerScripts {
    public class Player : MonoBehaviour {
        public static Player Instance = null;
        private Transform _transform;
        private Vector2 _movement;
        
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
        
        private void Start() {
            _transform = GetComponent<Transform>();
        }

        private void Update() {
            _movement = CInput.NavigationAxis;
            _transform.Translate(Time.deltaTime * 5 * new Vector2(_movement.x, _movement.y));
        }
    }
}