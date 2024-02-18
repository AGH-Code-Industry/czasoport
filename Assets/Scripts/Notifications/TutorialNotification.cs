using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNotification 
{
    public List<string> messages;

    public TutorialNotification(string message1, string message2, string message3) {
        messages = new List<string>(3);
        messages.Add(message1);
        messages.Add(message2);
        messages.Add(message3);
    }
}
