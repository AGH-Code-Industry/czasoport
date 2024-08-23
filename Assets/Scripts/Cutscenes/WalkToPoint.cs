using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene {
    public class WalkToPoint : MonoBehaviour {
        private Vector3 _start;
        [SerializeField] private Vector3 target;

        public void Walk(float time) {
            _start = transform.position;
            StartCoroutine(walking(time));
        }

        private IEnumerator walking(float time) {
            float timer = 0f;
            while (timer <= time) {
                transform.position = Vector3.Lerp(target, _start, (time - timer) / timer);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}