using System;
using CoinPackage.Debugging;
using UnityEngine;
using CustomInput;
using DataPersistence;
using LevelTimeChange;
using LevelTimeChange.LevelsLoader;
using Settings;

namespace PlayerScripts {
    public class Player : MonoBehaviour, IDataPersistence {
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

        public void LoadPersistentData(GameData gameData) {
            transform.position = gameData.playerGameData.position;
        }

        public void SavePersistentData(ref GameData gameData) {
            gameData.playerGameData.position = transform.position;
        }

        public override string ToString() {
            return $"[Player]" % Colorize.Gold;
        }
    }
}