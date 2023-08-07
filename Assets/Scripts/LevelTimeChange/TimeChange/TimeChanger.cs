using UnityEngine;
using System.Collections.Generic;
using CustomInput;
using UnityEngine.InputSystem;

namespace LevelTimeChange.TimeChange
{
    public class TimeChanger : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [Tooltip("Duration of the jump")] 
        [SerializeField] private float timeToChange = 0.3f;

        public TimeLine actualTime = TimeLine.Present;

        private List<CheckCollider> _boxes;
        [SerializeField]private Vector3 _timeJump; //PÓKI NIE MA ŁADOWANIE GRY
        private TimeLine _newTimeLine;

        private void Start() {
            _boxes = new List<CheckCollider>();
            //_timeJump = DeveloperSettings.Instance.tpcSettings.offsetFromPresentPlatform; // JAK BĘDZIE ŁADOWANIE GRY
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

                objectToSpawn.transform.position = i * _timeJump + transform.position;
                _boxes.Add(objectToSpawn.AddComponent<CheckCollider>());
            }
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
            TryChange(-1);

        }

        private void TimeForward(InputAction.CallbackContext ctx) {
            TryChange(1);
        }

        private void TryChange(int change) {
            if (actualTime == 0 && change == -1) change = 2;
            _newTimeLine = (TimeLine)(((int)actualTime + change) % 3);
            //CDebug.Log(new_id,Colorize.Magenta);
            if (CanChangeTime(_newTimeLine - actualTime))
            {
                StartCoroutine(ChangeTime());
            }
        }

        private IEnumerator<WaitForSeconds> ChangeTime() {
            var key = CInput.TeleportLock.Lock();
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(timeToChange/2);
            transform.Translate(_timeJump * (int)(_newTimeLine - actualTime));
            actualTime = _newTimeLine;
            animator.SetTrigger("End");
            yield return new WaitForSeconds(timeToChange/2);
            CInput.TeleportLock.Unlock(key);
        }

        private bool CanChangeTime(int when) {
            return _boxes[when + 2].IsNotTouching();
        }
    }
}
