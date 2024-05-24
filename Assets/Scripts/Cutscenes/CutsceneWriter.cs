using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneWriter : MonoBehaviour {
    public static CutsceneWriter Instance = null;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float typingSpeed = 100f;
    private bool isTyping = false;
    private string _fullText;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
        gameObject.SetActive(false);
    }

    public void StartTyping(string text) {
        _fullText = text;
        StartCoroutine(TypeText());
    }
    
    private IEnumerator TypeText() {
        textMeshPro.text = "";
        isTyping = true;
        foreach (char c in _fullText)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
