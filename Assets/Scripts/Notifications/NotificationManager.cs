using System;
using System.Collections;
using System.Collections.Generic;
using Notifications;
using UnityEngine;
using TMPro;
using Settings;
using System.Reflection;

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
    int _notificationsRaisen = 0;
    int _notificationsDisabled = 0;

    private void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    public void RaiseNotification(Notification notification) {
        if (String.IsNullOrEmpty(notification.message)) {
            return;
        }
        _notificationsRaisen++;
        _notificationMessage.text = notification.message;
        _notificationWindow.SetActive(true);
        Invoke("DisableNotificationWindow", notification.displayTime);
    }

    void DisableNotificationWindow() {
        if (_notificationsRaisen - _notificationsDisabled == 1) _notificationWindow.SetActive(false);
        _notificationsDisabled++;
    }

}
