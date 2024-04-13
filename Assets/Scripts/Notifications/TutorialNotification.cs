using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNotification {
    public List<string> messages;

    public TutorialNotification(List<string> messages) {
        if (messages.Count != 1 && messages.Count != 3) return;
        if (messages.Count == 3) {
            this.messages = new List<string>(3) {
                messages[0],
                messages[1],
                messages[2]
            };
        } else {
            this.messages = new List<string>(1) {
                messages[0]
            };
        }
    }

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