using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneWriter : MonoBehaviour {
    public static CutsceneWriter Instance = null;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float typingSpeed = 100f;
    private bool _isTyping = false;
    private Coroutine _current;
    private string _fullText;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
        gameObject.SetActive(false);
    }

    public void StartTyping(string text) {
        _fullText = text;
        if (_isTyping) {
            StopCoroutine(_current);
            _isTyping = false;
        }
        _current = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText() {
        textMeshPro.text = "";
        _isTyping = true;
        foreach (char c in _fullText) {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(typingSpeed);
        _isTyping = false;
    }
}