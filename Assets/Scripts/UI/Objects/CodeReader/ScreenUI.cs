using System;
using System.Collections;
using System.Collections.Generic;
using CustomInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour {
    [SerializeField] private int goodCode;
    [SerializeField] private TextMeshProUGUI codeScreen;
    [SerializeField] private GameObject finishGameUI;
    [SerializeField] private Button closeUIButton;
    [SerializeField] private GameObject codeUI;

    private Image _screenBackground;
    private Color _defaultColor;
    string _code = "";

    private void Awake() {
        closeUIButton.onClick.AddListener(CloseUI);
        _screenBackground = GetComponent<Image>();
        _defaultColor = _screenBackground.color;
        ClearCode();
    }

    private bool EndCode() {
        return _code.Length == 4;
    }
    
    public void WriteDigit(int digit) {
        if (EndCode()) {
            ShowError();
            return;
        }
        _code += digit;
        UpdateScreen();
    }
    
    public void DeleteLastDigit() {
        if (_code.Length == 0)
        {
            ShowError();
            return;
        }
        _code = _code.Remove(_code.Length - 1);
        UpdateScreen();
    }

    public void EnterCode() {
        CheckCode();
    }

    private void CheckCode() {
        if (int.Parse(_code) == goodCode) {
            ShowEndScreen();
        }
        else {
            BadCode();
        }
        ClearCode();
    }

    private void ClearCode() {
        _code = "";
        UpdateScreen();
    }

    private void UpdateScreen() {
        codeScreen.text = _code;
    }

    private void ShowEndScreen() {
        finishGameUI.SetActive(true);
    }

    private void BadCode() {
        ShowError();
    }

    private void ShowError() {
        LeanTween.color(_screenBackground.rectTransform, Color.red, 0.1f).setOnComplete(BackToDefaultColor);
    }

    private void BackToDefaultColor() {
        LeanTween.color(_screenBackground.rectTransform, _defaultColor, 0.2f);
    }

    private void CloseUI() {
        codeUI.SetActive(false);
        CInput.InputActions.Movement.Enable();
    }
}
