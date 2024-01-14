using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingLight : MonoBehaviour {

    public float lightChangeInterval = 5f;

    [SerializeField] private SpriteRenderer _lightSpriteRenderer;
    [SerializeField] private BoxCollider2D _crossingCollider;

    private readonly Color _greenLightColor = Color.green;
    private readonly Color _redLightColor = Color.red;

    private enum CrossingState {
        Opened,
        Closed
    }

    private CrossingState _crossingState;

    private void Start() {
        InvokeRepeating(nameof(ToggleLightsState), 0f, lightChangeInterval);
    }

    private void ToggleLightsState() {
        switch (_crossingState) {
            case CrossingState.Opened:
                CloseCrossing();
                break;
            case CrossingState.Closed:
                OpenCrossing();
                break;
        }
    }

    private void OpenCrossing() {
        _lightSpriteRenderer.color = _redLightColor;
        _crossingCollider.enabled = false;
        _crossingState = CrossingState.Opened;
    }

    private void CloseCrossing() {
        _lightSpriteRenderer.color = _greenLightColor;
        _crossingCollider.enabled = true;
        _crossingState = CrossingState.Closed;
    }
}