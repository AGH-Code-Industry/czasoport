using System.Collections;
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
        [SerializeField] private float restTime = 3f;
        [SerializeField] private List<Transform> marchPoints = new();
        [SerializeField] private bool loop = true;
        private int _previousTarget = 0;
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
            if (walkOnStart) StartWalk();
            else StopWalk();
            
        }

        private void Update() {
            if (!_canWalk) {
                if (Random.Range(0f,1f) < 0.5f) _animator.SetTrigger("Wink");
                return;
            }
            //Walking
            if (_walkProgress < 1f) {
                _walkProgress += (Time.deltaTime/_walkDistance) * walkSpeed;
                _transform.position = Vector3.Lerp(marchPoints[_previousTarget].position, marchPoints[_actualTarget].position, _walkProgress);
            } else {
                StartCoroutine(Rest(restTime));
            }
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

        private IEnumerator Rest(float delay) {
            StopWalk();
            _animator.ResetTrigger("Wink");
            _walkProgress = 0f;
            _previousTarget = _actualTarget;
            if (!loop) {
                marchPoints.RemoveAt(_previousTarget);
            }
            _actualTarget = (_actualTarget+1) % marchPoints.Count;
            yield return new WaitForSeconds(delay);
            StartWalk();
        }
    }
}