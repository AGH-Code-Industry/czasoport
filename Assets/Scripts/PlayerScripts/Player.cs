using System;
using UnityEngine;
using CustomInput;
using Settings;

namespace PlayerScripts {
    public class Player : MonoBehaviour {
        public static Player Instance = null;

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
            transform.Translate(Time.deltaTime * _settings.movementSpeed * new Vector2(movement.x, movement.y));
       }
    }
}