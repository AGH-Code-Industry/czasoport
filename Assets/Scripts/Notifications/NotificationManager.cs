﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Settings;

public class NotificationManager : MonoBehaviour {

    /* If you want to display a notification use this formula:
     NotificationManager.Instance.RaiseNotification(new Notification(string message, float displayTime));
    You can always change the Nofication class to change it's properties.
    */
    public static NotificationManager Instance;

    [SerializeField]
    GameObject _notificationWindow;
    [SerializeField]
    TMP_Text _notificationMessage;

    private void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    public void RaiseNotification(Notification notificationToDisplay) {
        _notificationMessage.text = notificationToDisplay.message;
        _notificationWindow.SetActive(true);
        Invoke("DisableNotificationWindow", notificationToDisplay.displayTime);
    }

    void DisableNotificationWindow() {
        _notificationWindow.SetActive(false);
    }

}
