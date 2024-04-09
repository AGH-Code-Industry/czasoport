using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInArea : MonoBehaviour {
    public bool IsInArea = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) IsInArea = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) IsInArea = false;
    }
}