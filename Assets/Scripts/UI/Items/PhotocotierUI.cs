using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Interactions;
using System;

public class PhotocotierUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _documentCounter;

    private void Start() {
        _documentCounter.gameObject.SetActive(false);
        this.gameObject.GetComponent<HighlightInteraction>().onHighlightStart += displayText;
        this.gameObject.GetComponent<HighlightInteraction>().onHighlightEnd += stopDisplayText;
    }

    private void stopDisplayText(object sender, InteractionHighlightEventArgs e) {
        if (e.senderObject.Equals(this.gameObject)) _documentCounter.gameObject.SetActive(false);
    }

    private void displayText(object sender, InteractionHighlightEventArgs e) {
        if (e.senderObject.Equals(this.gameObject)) _documentCounter.gameObject.SetActive(true);
    }

    public void setCounter(String counterText) {
        _documentCounter.text = counterText;
    }
}