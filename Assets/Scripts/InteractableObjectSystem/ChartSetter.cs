using System;
using System.Collections;
using System.Collections.Generic;
using InteractableObjectSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChartSetter : InteractableObject
{
    [SerializeField] private GameObject _chartSetterUI;
    [SerializeField] private Button _closeButton;
    [SerializeField] private UILineRenderer _userLineRenderer;
    [SerializeField] private UILineRenderer _goodLine;
    [SerializeField] private Button _upArrow;
    [SerializeField] private Button _downArrow;
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private RafineryKey _rafineryKey;

    private bool _block;
    private int resolution = 1000;
    private float length = 600f;
    private float userAmplitude = 160f;
    private float userFrequency = 30f;

    private float goodAmplitude = 100f;
    private float goodFrequency = 10f;


    private void Awake() {
        _closeButton.onClick.AddListener(CloseChartSetter);
        _upArrow.onClick.AddListener(delegate { ChangeAmplitude(1);});
        _downArrow.onClick.AddListener(delegate { ChangeAmplitude(-1);});
        _leftArrow.onClick.AddListener(delegate { ChangeFrequency(-1);});
        _rightArrow.onClick.AddListener(delegate { ChangeFrequency(1);});
        GenerateGoodSineWave();
        GenerateUserSineWave();
        CloseChartSetter();
    }

    public override void InteractionHand() {
        _chartSetterUI.SetActive(!_chartSetterUI.activeSelf);
    }

    private void CloseChartSetter() {
        _chartSetterUI.SetActive(false);
    }

    private void GenerateGoodSineWave()
    {
        for (int i = 0; i < resolution; i++)
        {
            float x = i * (length / (resolution - 1));
            float y = goodAmplitude * Mathf.Sin(2 * Mathf.PI * goodFrequency * x);

            _goodLine.points.Add(new Vector3(x, y, 0));
        }
    }

    private void GenerateUserSineWave() {
        if (_block) return;
        _userLineRenderer.points.RemoveAll(_ => true);
        _userLineRenderer.gameObject.SetActive(false);
        for (int i = 0; i < resolution; i++)
        {
            float x = i * (length / (resolution - 1));
            float y = userAmplitude * Mathf.Sin(2 * Mathf.PI * userFrequency * x);

            _userLineRenderer.points.Add(new Vector3(x, y, 0));
        }
        _userLineRenderer.gameObject.SetActive(true);
        if (userAmplitude == goodAmplitude && userFrequency == goodFrequency) {
            SetGoodItem();
            _block = true;
        }
    }

    private void SetGoodItem() {
        _rafineryKey.SetGoodItem();
    }

    private void ChangeAmplitude(int direction) {
        userAmplitude += 10f * direction;
        GenerateUserSineWave();
    }

    private void ChangeFrequency(int direction) {
        userFrequency += 5f * direction;
        GenerateUserSineWave();
    }
}
