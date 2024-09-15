using System;
using System.Collections;
using System.Collections.Generic;
using LevelTimeChange;
using LevelTimeChange.TimeChange;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TimeChangeUI : MonoBehaviour {

        [SerializeField] private TimeUIToogle past;
        [SerializeField] private TimeUIToogle pastStroke;
        [SerializeField] private TimeUIToogle present;
        [SerializeField] private TimeUIToogle presentStroke;
        [SerializeField] private TimeUIToogle future;
        [SerializeField] private TimeUIToogle futureStroke;
        [SerializeField] private GameObject Camouflage;
        [SerializeField] private Image TimeChangerBackground;
        [SerializeField] private Sprite TwoOrbsBackground;
        [SerializeField] private Sprite ThreeOrbsBackground;

        private TimeUIToogle currentUI;
        private Transform beginPosition;
        private bool isMoving = false;

        public void UnlockTimeUI() {
            TimeChanger.Instance.OnTimeChange += TimeChanger_OnTimeChange;
            TimeChanger.Instance.OnTryTimeChange += Instance_OnTryTimeChange;
            TimeChanger.Instance.OnTeleportationEnded += (object sender, EventArgs e) => CheckTeleportAbilities();
            Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;

            ChangeSelectedTime(TimeChanger.Instance.actualTime);
        }

        private void Instance_OnTryTimeChange(object sender, TimeChanger.OnTimeChangeEventArgs e) {
            beginPosition = currentUI.transform;
            if (isMoving) return;
            isMoving = true;
            StartCoroutine(waiter(0.1f));
            if (e.previousTime < e.time) {
                LeanTween.moveLocalX(currentUI.gameObject, currentUI.transform.GetComponent<RectTransform>().localPosition.x + 40, 0.05f)
                    .setOnComplete(() => {
                        LeanTween.moveLocalX(currentUI.gameObject, currentUI.transform.GetComponent<RectTransform>().localPosition.x - 40, 0.05f);
                    });
            } else {
                LeanTween.moveLocalX(currentUI.gameObject, currentUI.transform.GetComponent<RectTransform>().localPosition.x - 40, 0.05f)
                    .setOnComplete(() => {
                        LeanTween.moveLocalX(currentUI.gameObject, currentUI.transform.GetComponent<RectTransform>().localPosition.x + 40, 0.05f);
                    });
            }
            currentUI.transform.position = beginPosition.position;
        }

        IEnumerator waiter(float seconds) {
            yield return new WaitForSeconds(seconds);
            isMoving = false;
        }

        public void UpdateTimeUI() {
            ChangeSelectedTime(TimeChanger.Instance.actualTime);
        }

        private void Player_OnPlayerMoved(object sender, EventArgs e) {
            CheckTeleportAbilities();
        }

        private void CheckTeleportAbilities() {
            CheckTeleportAbility(past, pastStroke, TimeLine.Past);
            CheckTeleportAbility(present, presentStroke, TimeLine.Present);
            CheckTeleportAbility(future, futureStroke, TimeLine.Future);
        }
        private void TimeChanger_OnTimeChange(object sender, TimeChanger.OnTimeChangeEventArgs e) {
            ChangeSelectedTime(e.time);
        }

        private void ChangeSelectedTime(TimeLine actualTime) {
            ToggleActualTime(past, pastStroke, actualTime == TimeLine.Past);
            ToggleActualTime(present, presentStroke, actualTime == TimeLine.Present);
            ToggleActualTime(future, futureStroke, actualTime == TimeLine.Future);
            CheckTeleportAbilities();
        }

        private void ToggleActualTime(TimeUIToogle timeImage, TimeUIToogle timeStroke, bool actual) {
            if (actual) currentUI = timeImage;
            timeImage.SetStroke(actual);
            timeStroke.SetStroke(actual);
        }

        private void CheckTeleportAbility(TimeUIToogle timeImage, TimeUIToogle timeStroke, TimeLine time) {
            if (time == TimeChanger.Instance.actualTime)
                return;

            ToggleBlockedTime(timeImage, timeStroke, !TimeChanger.Instance.CanChangeTime(time));
        }

        private void ToggleBlockedTime(TimeUIToogle timeImage, TimeUIToogle timeStroke, bool block) {
            timeStroke.SetStroke(!block);
        }

        public void ChangeInvBackground(TimeLine UnlockedTime) {
            if (UnlockedTime == TimeLine.Future) {
                TimeChangerBackground.sprite = TwoOrbsBackground;
            }
            else if (UnlockedTime == TimeLine.Past) {
                Camouflage.SetActive(false);
                TimeChangerBackground.sprite = ThreeOrbsBackground;
            }
        }
    }
}