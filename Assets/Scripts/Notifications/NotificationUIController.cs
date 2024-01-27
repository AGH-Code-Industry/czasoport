using Notifications;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class NotificationUIController : MonoBehaviour {
    [SerializeField] GameObject _notificationHistoryPanel;
    [SerializeField] GameObject _notificationWindow;
    
    public void OpenNotificationPanel() {
        _notificationHistoryPanel.SetActive(true);
        _notificationWindow.SetActive(false);
    }

    public void CloseNotificationPanel() {
        _notificationHistoryPanel.SetActive(false);
        _notificationWindow.SetActive(true);
    }

   
}
