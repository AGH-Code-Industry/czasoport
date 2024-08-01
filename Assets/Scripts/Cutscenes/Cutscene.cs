using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomInput;
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

        private Queue<string> _keys = new();

        private void Start() {
            _writer = CutsceneWriter.Instance;
            _events = timeSchedule.Zip(incidents, (t, i) => (t, i)).ToList(); ;
        }

        public void Update() {
            if (Input.GetKeyDown("x")) StartScene();
        }

        public void StartScene() {
            StartCoroutine(Scene());
        }

        private IEnumerator Scene() {
            Lock();
            ShowWriter(true);
            foreach (var e in _events) {
                e.Item2.Invoke();
                yield return new WaitForSeconds(e.Item1);
            }
            ShowWriter(false);
            Unlock();
        }

        private void ShowWriter(bool show) {
            _writer.gameObject.SetActive(show);
        }

        public void TypeText(string text) {
            _writer.StartTyping(text);
        }

        private void Lock() {
            _keys.Enqueue(CInput.InteractionsLock.Lock());
            _keys.Enqueue(CInput.MovementLock.Lock());
            _keys.Enqueue(CInput.MouseLock.Lock());
            _keys.Enqueue(CInput.TeleportLock.Lock());
            _keys.Enqueue(CInput.InteractionsLock.Lock());
        }

        private void Unlock() {
            CInput.InteractionsLock.Unlock(_keys.Dequeue());
            CInput.MovementLock.Unlock(_keys.Dequeue());
            CInput.MouseLock.Unlock(_keys.Dequeue());
            CInput.TeleportLock.Unlock(_keys.Dequeue());
            CInput.InteractionsLock.Unlock(_keys.Dequeue());
        }
    }
}