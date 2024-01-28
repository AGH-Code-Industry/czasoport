using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Notifications {
    [System.Serializable]
    public class Notification
    {
        public string message;
        public float displayTime = 3.0f;

        public Notification(string message, float displayTime) {
            this.message = message;
            this.displayTime = displayTime;
        }
    }
}

