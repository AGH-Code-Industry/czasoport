using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using NPC;
using UnityEngine;

public class CrossingLight : MonoBehaviour {

    public float lightChangeInterval = 5f;

    [SerializeField] private SpriteRenderer _lightSpriteRenderer;
    [SerializeField] private List<PathWalking> _pathWalkings = new List<PathWalking>();

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
        _crossingState = CrossingState.Opened;
        foreach (PathWalking pw in _pathWalkings) {
            pw.ContinueWalk();
        }
    }

    private void CloseCrossing() {
        _lightSpriteRenderer.color = _greenLightColor;
        _crossingState = CrossingState.Closed;
        foreach (PathWalking pw in _pathWalkings) {
            pw.StopWalk();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (_crossingState == CrossingState.Opened) return;
        if (other.CompareTag("NPC")) {
            PathWalking pw = null;
            pw = other.GetComponent<PathWalking>();
            pw.StopWalk();
            if (pw != null) _pathWalkings.Add(pw);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("NPC")) {
            PathWalking pw = null;
            pw = other.GetComponent<PathWalking>();
            if (pw != null) _pathWalkings.Remove(pw);
        }
    }
}