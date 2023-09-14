using System;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        private static Player _instance;

        private void Awake() {
            if (_instance == null) {
                _instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public static Player Instance {
            get {
                if (_instance == null) throw new NullReferenceException();
                return _instance;
            }
        }

        private void OnDestroy() {
            if (_instance == this) {
                _instance = null;
            }
        }
    }
}