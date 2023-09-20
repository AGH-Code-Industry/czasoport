using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  UI {
    public class TimeChangeUI : MonoBehaviour {
        
        [SerializeField] private Image past;
        [SerializeField] private Image present;
        [SerializeField] private Image future;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color disableTeleportColor;

        private void Awake() {
            present.color = selectedColor;
            past.color = disableTeleportColor;
        }
    }    
}

