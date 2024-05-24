using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Cutscene {
    public class Cutscene : MonoBehaviour {
        /// <summary>
        /// Scriptable object for cutscene
        /// </summary>

        //Time to start next
        public List<float> timeSchedule = new();

        //Thing to do
        public List<UnityEvent> incidents = new();

        private List<(float, UnityEvent)> _events;
        private CutsceneWriter _writer;
        
        private void Start() {
            _writer = CutsceneWriter.Instance;
            _events = timeSchedule.Zip(incidents, (t, i) => (t, i)).ToList(); ;
        }

        public void Update() {
            if(Input.GetKeyDown("x")) StartScene();
        }

        public void StartScene() {
            StartCoroutine(Scene());
        }

        private IEnumerator Scene() {
            showWriter(true);
            foreach (var e in _events) {
                e.Item2.Invoke();
                yield return new WaitForSeconds(e.Item1);
            }
            showWriter(false);
        }

        private void showWriter(bool show) {
            _writer.gameObject.SetActive(show);
        }
        
        public void TypeText(string text) {
            _writer.StartTyping(text);
        }
    }
}