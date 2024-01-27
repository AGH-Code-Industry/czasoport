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

        private Rigidbody2D _rigid;
        private PlayerSettingsSO _settings;
        Animator _animator;

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
            _animator = GetComponent<Animator>();
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Update() {
            var movement = CInput.NavigationAxis;
            _animator.SetFloat("Horizontal", movement.x);
            _animator.SetFloat("Vertical", movement.y);
            float _speed = movement.sqrMagnitude;
            _animator.SetFloat("Speed", _speed);
            if (_speed > 0.01) {
                _animator.SetFloat("LastHorizontal", movement.x);
                _animator.SetFloat("LastVertical", movement.y);
            }
            //transform.Translate(Time.deltaTime * _settings.movementSpeed * new Vector2(movement.x, movement.y));
            _rigid.AddForce(Time.deltaTime * 1000 * _settings.movementSpeed * movement);

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