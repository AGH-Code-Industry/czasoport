using System;
using System.Collections;
using System.Collections.Generic;
using CustomInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreenUI : MonoBehaviour {
    [SerializeField] private Button backToMenuButton;

    private void Awake() {
        backToMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("EntryPoint");
            CInput.InputActions.Movement.Enable();
        });
    }
}
