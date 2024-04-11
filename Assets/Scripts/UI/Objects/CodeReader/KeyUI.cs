using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour {
    [SerializeField] private ScreenUI screenUI;

    enum ButtonType {
        Key,
        Enter,
        Back,
    }

    [SerializeField] private ButtonType type;

    private void Awake() {
        Button button = GetComponent<Button>();
        string textOnButton = GetComponentInChildren<TextMeshProUGUI>().text;
        switch (type) {
            case ButtonType.Key:
                button.onClick.AddListener(() => screenUI.WriteDigit(int.Parse(textOnButton)));
                break;
            case ButtonType.Enter:
                button.onClick.AddListener(() => screenUI.EnterCode());
                break;
            case ButtonType.Back:
                button.onClick.AddListener(() => screenUI.DeleteLastDigit());
                break;
        }
    }
}