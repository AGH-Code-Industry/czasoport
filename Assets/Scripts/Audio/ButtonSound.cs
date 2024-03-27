using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

    public AudioSource buttonClickSound; // Add an AudioSource component to your GameObject and assign it here in the Inspector.
    // Start is called before the first frame update

    void PlayButtonClickSound() {
        if (buttonClickSound != null) {
            buttonClickSound.Play(); // Play the assigned click sound.
        }
    }
}