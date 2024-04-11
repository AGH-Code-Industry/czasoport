using System;
using System.Collections;
using System.Collections.Generic;
using CustomInput;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreenUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button backToMenuButton;

    private void Awake() {
        backToMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("EntryPoint");
            CInput.InputActions.Movement.Enable();
        });
    }

    public void ShowTime() {
        float time = Timer.instance.GetMeasuredTime();
        Timer.instance.ResetTimer();
        string timeText = Mathf.Floor(time / 60f) + " minutes " + Mathf.Floor(time % 60f) + " seconds";
        timerText.text = timeText;
    }
}