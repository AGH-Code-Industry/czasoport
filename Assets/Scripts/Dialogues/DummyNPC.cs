using System;
using System.Collections;
using System.Collections.Generic;
using Dialogues;
using UnityEngine;

public class DummyNPC : MonoBehaviour {
    private bool inRange = false;
    public TextAsset dialogue;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.O) && inRange) {
            DialogueManager.I.StartDialogue(dialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        inRange = false;
    }
}