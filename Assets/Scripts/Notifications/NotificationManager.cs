using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Settings;

public class NotificationManager : MonoBehaviour {
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

    public void RaiseNotification(string notificationToDisplay) {
        _notificationMessage.text = notificationToDisplay;
        _notificationWindow.SetActive(true);
        Invoke("DisableNotificationWindow", DeveloperSettings.Instance.appSettings.notificationDuration);
    }

    void DisableNotificationWindow() {
        _notificationWindow.SetActive(false);
    }

}
