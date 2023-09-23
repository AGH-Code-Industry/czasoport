using System;
using UnityEngine;
using CustomInput;
using Settings;

namespace PlayerScripts {
    public class Player : MonoBehaviour {
        public static Player Instance = null;

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
            NotificationManager.Instance.RaiseNotification(NotificationManager.notification.NoInventoryRoom);
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
        }
    }
}