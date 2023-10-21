using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification
{
    public string message;
    public float displayTime;

    public Notification(string message, float displayTime) {
        this.message = message;
        this.displayTime = displayTime;
    }
}
