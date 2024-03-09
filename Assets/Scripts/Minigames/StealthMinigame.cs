using System;
using System.Collections;
using System.Collections.Generic;
using LevelTimeChange.TimeChange;
using PlayerScripts;
using Settings;
using UnityEngine;

namespace Minigames {
    public class StealthMinigame : MonoBehaviour {
        [SerializeField] private CheckPlayerInArea _area;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Animator animator;
        private float _animationTime;

        private bool _playerInArea {
            get {
                return _area.IsInArea;
            }
        }
        private void Start() {
            animator = TimeChanger.Instance.animator;
            _animationTime = DeveloperSettings.Instance.tpcSettings.timelineChangeAnimLength;
        }

        public void RestartMinigame() {
            if (_playerInArea) StartCoroutine(SetPlayerToStart());
        }

        private IEnumerator SetPlayerToStart() {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(_animationTime/2);
            Player.Instance.transform.position = startPoint.position;
            yield return new WaitForSeconds(_animationTime/2);
            animator.SetTrigger("End");
        }
    }
}