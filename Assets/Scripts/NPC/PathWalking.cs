using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;

namespace NPC {
    public class PathWalking : MonoBehaviour {
        private Transform _transform;
        
        [SerializeField] private bool walkOnStart = true;
        [SerializeField] private float walkSpeed = 1f;
        private bool _canWalk = false;

        [SerializeField] private List<Transform> marchPoints = new List<Transform>();
        private int _previousTarget = 0;
        private int _actualTarget = 1;
        private float _walkProgress = 0f;
        private float _walkDistance;

        private void Start() {
            if (marchPoints.Count <= 1) {
                CDebug.LogError("Path contains 1 point or less");
                return;
            }

            _transform = transform;
            _canWalk = walkOnStart;
            _walkDistance = Vector3.Distance(marchPoints[_actualTarget - 1].position,
                marchPoints[_actualTarget].position);
        }

        private void Update() {
            if (!_canWalk) return;
            //Walking
            if (_walkProgress < 1f) {
                _walkProgress += (Time.deltaTime/_walkDistance) * walkSpeed;
                _transform.position = Vector3.Lerp(marchPoints[_previousTarget].position, marchPoints[_actualTarget].position, _walkProgress);
            } else {
                _walkProgress = 0f;
                _previousTarget = _actualTarget;
                _actualTarget = (_actualTarget+1) % marchPoints.Count;
                _walkDistance = Vector3.Distance(marchPoints[_previousTarget].position,
                    marchPoints[_actualTarget].position);
            }
            //Animation?
        }

        /// <summary>
        /// Stop npc
        /// </summary>
        public void StopWalk() {
            _canWalk = false;
        }

        /// <summary>
        /// Npc continues walk by path
        /// </summary>
        public void ContinueWalk() {
            _canWalk = true;
        }

    }
}