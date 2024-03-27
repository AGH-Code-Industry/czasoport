using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static Timer instance;

    private void Awake() {
        instance = this;
    }

    private float _elapsedTime = 0f;
    private bool _timerOn = false;

    public void StartTimer() {
        _timerOn = true;
    }

    public void StopTimer() {
        _timerOn = false;
    }

    public void ResetTimer() {
        _elapsedTime = 0f;
    }

    public float GetMeasuredTime() {
        return _elapsedTime;
    }

    void Update() {
        if (_timerOn) {
            _elapsedTime += Time.unscaledDeltaTime;
        }
    }
}