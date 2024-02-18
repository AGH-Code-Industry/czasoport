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
        [SerializeField] private Image present;
        [SerializeField] private Image future;

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
            CheckTeleportAbility(past, TimeLine.Past);
            CheckTeleportAbility(present, TimeLine.Present);
            CheckTeleportAbility(future, TimeLine.Future);
        }
        private void TimeChanger_OnTimeChange(object sender, TimeChanger.OnTimeChangeEventArgs e) {
            ChangeSelectedTime(e.time);
        }

        private void ChangeSelectedTime(TimeLine actualTime) {
            ToggleActualTime(past, actualTime == TimeLine.Past);
            ToggleActualTime(present, actualTime == TimeLine.Present);
            ToggleActualTime(future, actualTime == TimeLine.Future);
            CheckTeleportAbilities();
        }

        private void ToggleActualTime(Image timeImage, bool actual) {
            timeImage.color = actual ? selectedColor : timeImage.color;
        }

        private void CheckTeleportAbility(Image timeImage, TimeLine time) {
            if (time == TimeChanger.Instance.actualTime)
                return;
            
            ToggleBlockedTime(timeImage, !TimeChanger.Instance.CanChangeTime(time));
        }

        private void ToggleBlockedTime(Image timeImage, bool block) {
            timeImage.color = block ? disableTeleportColor : Color.white;
        }

    }
}