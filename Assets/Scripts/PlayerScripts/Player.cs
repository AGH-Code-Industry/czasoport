using System;
using UnityEngine;
using CustomInput;
using Settings;

namespace PlayerScripts {
    public class Player : MonoBehaviour {
        public static Player Instance { get; private set; }

        public event EventHandler OnPlayerMoved;

        private PlayerSettingsSO _settings;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            _settings = DeveloperSettings.Instance.playerSettings;
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Update() {
            var movement = CInput.NavigationAxis;
            transform.Translate(Time.deltaTime * _settings.movementSpeed * new Vector2(movement.x, movement.y));
            if (movement != Vector2.zero)
                OnPlayerMoved?.Invoke(this, EventArgs.Empty);
        }
    }
}