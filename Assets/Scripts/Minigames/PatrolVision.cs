using System;
using UnityEngine;

namespace Minigames {
    public class PatrolVision : MonoBehaviour {
        [SerializeField] private StealthMinigame stealthMissionManager;
        private bool _toMinigame = true;
        private Transform _parent;
        private Vector3 _previousPosition;
        private Vector3 _direction;

        private void Start() {
            _parent = transform.parent;
            _previousPosition = _parent.position;
            if (stealthMissionManager is not null) _toMinigame = true;
        }

        private void Update() {
            _direction = (_previousPosition - _parent.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(transform.forward, _direction);
            transform.rotation = rotation;
            _previousPosition = _parent.position;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                stealthMissionManager.RestartMinigame();
            }
        }
    }
}