using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    Transform _player;
    [SerializeField]
    GameObject _notificationWindow;
    [SerializeField]
    TMP_Text _notificationMessage;

    public static NotificationManager Instance;

    public Notification NoInventoryRoom = new Notification("There is no more room in the inventory");
    public Notification WrongKey = new Notification("You used wrong key");

    public enum notification {
        NoInventoryRoom,
        WrongKey
    }

    public Dictionary<notification, Notification> notifications = new Dictionary<notification, Notification>();


    private void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
        notifications.Add(notification.NoInventoryRoom, NoInventoryRoom);
        notifications.Add(notification.WrongKey, WrongKey);
    }

    public void RaiseNotification(notification notificationToDisplay) {
        _notificationMessage.text = notifications[notificationToDisplay].message;
        _notificationWindow.SetActive(true);
        Invoke("DisableNotificationWindow", 5f);
    }

    void DisableNotificationWindow() {
        _notificationWindow.SetActive(false);
    }

}
