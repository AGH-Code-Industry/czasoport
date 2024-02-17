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

        [SerializeField] private Image past;
        [SerializeField] private TimeUIStroke pastStroke;
        [SerializeField] private Image present;
        [SerializeField] private TimeUIStroke presentStroke;
        [SerializeField] private Image future;
        [SerializeField] private TimeUIStroke futureStroke;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color disableTeleportColor;

        private void Start() {
            TimeChanger.Instance.OnTimeChange += TimeChanger_OnTimeChange;
            Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;
            
            ChangeSelectedTime(TimeChanger.Instance.actualTime);
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

        private void ToggleActualTime(Image timeImage, TimeUIStroke timeStroke,  bool actual) {
            timeImage.color = actual ? selectedColor : timeImage.color;
            timeStroke.SetStroke(actual);
        }

        private void CheckTeleportAbility(Image timeImage, TimeUIStroke timeStroke,  TimeLine time) {
            if (time == TimeChanger.Instance.actualTime)
                return;
            
            ToggleBlockedTime(timeImage, timeStroke, !TimeChanger.Instance.CanChangeTime(time));
        }

        private void ToggleBlockedTime(Image timeImage, TimeUIStroke timeStroke, bool block) {
            timeImage.color = block ? disableTeleportColor : Color.white;
            timeStroke.SetStroke(!block);
        }

    }
}