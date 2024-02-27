using System.Collections.Generic;
using CoinPackage.Debugging;
using UnityEngine;

namespace NPC {
    public class PathWalking : MonoBehaviour {
        private Transform _transform;
        private Animator _animator;
        
        [SerializeField] private bool walkOnStart = true;
        [SerializeField] private float walkSpeed = 1f;
        private bool _canWalk;

        [SerializeField] private List<Transform> marchPoints = new();
        private int _previousTarget;
        private int _actualTarget = 1;
        private float _walkProgress;
        private float _walkDistance;

        private void Start() {
            
            if (marchPoints.Count <= 1) {
                CDebug.LogError("Path contains 1 point or less");
                return;
            }
            _animator = GetComponent<Animator>();
            _transform = transform;
            _canWalk = walkOnStart;
            if (_canWalk) {
                _walkDistance = Vector2.Distance(marchPoints[_previousTarget].position,
                    marchPoints[_actualTarget].position);
                _animator.SetFloat("Horizontal",
                    marchPoints[_actualTarget].position.x - marchPoints[_previousTarget].position.x);
                _animator.SetFloat("Vertical",
                    marchPoints[_actualTarget].position.y - marchPoints[_previousTarget].position.y);
                _animator.SetFloat("Speed", walkSpeed);
            } else {
                _animator.SetFloat("Horizontal",0);
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Speed", 0);
            }
            
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
                _walkDistance = Vector2.Distance(marchPoints[_previousTarget].position,marchPoints[_actualTarget].position);
                _animator.SetFloat("Horizontal", marchPoints[_actualTarget].position.x - marchPoints[_previousTarget].position.x);
                _animator.SetFloat("Vertical", marchPoints[_actualTarget].position.y - marchPoints[_previousTarget].position.y);
            }
            //Animation?
        }

        /// <summary>
        /// Stop npc
        /// </summary>
        public void StopWalk() {
            _canWalk = false;
            _animator.SetFloat("Speed", 0);
            _animator.SetFloat("Horizontal",0);
            _animator.SetFloat("Vertical", 0);
        }

        /// <summary>
        /// Npc continues walk by path
        /// </summary>
        public void StartWalk() {
            _canWalk = true;
            _walkDistance = Vector2.Distance(marchPoints[_previousTarget].position,marchPoints[_actualTarget].position);
            _animator.SetFloat("Speed", walkSpeed);
            _animator.SetFloat("Horizontal", marchPoints[_actualTarget].position.x - marchPoints[_previousTarget].position.x);
            _animator.SetFloat("Vertical", marchPoints[_actualTarget].position.y - marchPoints[_previousTarget].position.y);
        }

    }
}