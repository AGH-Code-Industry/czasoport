using System;
using CoinPackage.Debugging;
using UnityEngine;
using System.Collections.Generic;
using InputSystem;
using UnityEngine.InputSystem;

namespace TimeChange
{
    public class TimeChanger : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Vector3 timeJump;
        [SerializeField] private float timeToChange = 0.3f;

        public TimeLine actualTime = TimeLine.Present;

        private delegate void ChangingTime(); 
        private ChangingTime TCT;
        
        private List<CheckCollider> _boxes;
        private TimeLine _newTimeLine;
        private float _counterToChange;
        private int _change;
        
        private void Start() {
            _boxes = new List<CheckCollider>();

            for (int i = -2; i <= 2; i++)
            {
                if (i == 0)
                {
                    _boxes.Add(null);
                    continue;
                }

                GameObject objectToSpawn = new GameObject("ColliderToSpawn");
                objectToSpawn.transform.parent = this.gameObject.transform;
                objectToSpawn.AddComponent<BoxCollider2D>();

                objectToSpawn.transform.position = i * timeJump + transform.position;
                _boxes.Add(objectToSpawn.AddComponent<CheckCollider>());
            }
            _counterToChange = timeToChange;
        }

        private void Update() {
            if (TCT != null) TCT();
        }

        private void OnEnable()
        {
            CInput.InputActions.Teleport.TeleportBack.performed += TimeBack;
            CInput.InputActions.Teleport.TeleportForward.performed += TimeForward;
        }

        private void OnDisable() {
            CInput.InputActions.Teleport.TeleportBack.performed -= TimeBack;
            CInput.InputActions.Teleport.TeleportForward.performed -= TimeForward;
        }

        private void TimeBack(InputAction.CallbackContext ctx) {
            _change = -1;
            TCT += TryChange;
            CInput.InputActions.Teleport.Disable();
        }

        private void TimeForward(InputAction.CallbackContext ctx) {
            _change = 1;
            TCT += TryChange;
            CInput.InputActions.Teleport.Disable();
        }

        private void TryChange() {
            if (actualTime == 0 && _change == -1) _change = 2;
            _newTimeLine = (TimeLine)(((int)actualTime + _change) % 3);
            //CDebug.Log(new_id,Colorize.Magenta);
            _change = 0;
            if (CanChangeTime(_newTimeLine - actualTime))
            {
                animator.SetTrigger("Start");
                TCT += ChangeTime;
            }
            else
            {
                CInput.InputActions.Teleport.Enable();
            }

            TCT -= TryChange;
        }

        private void ChangeTime() {
            _counterToChange -= Time.deltaTime;
            if (_counterToChange < 0f)
            {
                transform.Translate(timeJump * (int)(_newTimeLine - actualTime));
                actualTime = _newTimeLine;
                animator.SetTrigger("End");

                _counterToChange = timeToChange;

                CInput.InputActions.Teleport.Enable();
                TCT -= ChangeTime;
            }
        }

        private bool CanChangeTime(int when) {
            return _boxes[when + 2].IsNotTouching();
        }
    }
}
