using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNotification {
    public List<string> messages;

    public TutorialNotification(string message1, string message2, string message3) {
        messages = new List<string>(3) {
            message1,
            message2,
            message3
        };
    }

    public TutorialNotification(string message) {
        messages = new List<string>(1) {
            message
        };
    }
}