using System;
using System.Collections;
using System.Collections.Generic;
using Notifications;
using UnityEngine;
using TMPro;
using Settings;
using DataPersistence;
using UnityEngine.UI;
using UI;
using CustomInput;
using UnityEngine.InputSystem;

public class NotificationManager : MonoBehaviour, IDataPersistence {

    /* If you want to display a notification use this formula:
     NotificationManager.Instance.RaiseNotification(new Notification(string message, float displayTime));
    You can always change the Nofication class to change it's properties.
    */
    public static NotificationManager Instance;

    [SerializeField]
    TMP_Text _notificationMessage;
    /*[SerializeField]
    GameObject _scrollContent;
    [SerializeField]
    GameObject _logPrerab;*/
    [SerializeField]
    GameObject _notification;
    [SerializeField]
    GameObject _tutorialNotification;
    [SerializeField]
    GameObject _notificationWindow;
    [SerializeField]
    TMP_Text _bigMessage;

    [SerializeField]
    GameObject _messageGo1;
    [SerializeField]
    GameObject _messageGo2;
    [SerializeField]
    GameObject _messageGo3;

    Queue<Notification> _notificationsToDisplay = new Queue<Notification>();
    List<Notification> _notificationsHistory = new List<Notification>();
    bool _isNotificationDisplaying = false;

    PauseUI _pauseUI;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        _pauseUI = FindObjectOfType<PauseUI>();
        _messageGo1.GetComponent<TMP_Text>().text = "";
        CInput.InputActions.Game.TogglePause.performed += PausePerformed;
        LeanTween.scale(_messageGo2.gameObject, new Vector3(1.2f, 1.2f, 1f), 0.5f).setLoopPingPong();
        _tutorialNotification.SetActive(false);
        _bigMessage.enabled = false;
        _notificationWindow.SetActive(false);
        _notification.SetActive(true);
    }

    private void PausePerformed(InputAction.CallbackContext context) {
        if (_pauseUI.IsGamePaused() && _notificationWindow.activeSelf == true) {
            _notificationWindow?.SetActive(false);
        }
    }

    public void RaiseNotification(Notification notification) {
        if (String.IsNullOrEmpty(notification.message)) {
            return;
        }
        _notificationsToDisplay.Enqueue(notification);
        if (!_isNotificationDisplaying) {
            _isNotificationDisplaying = true;
            StartCoroutine(DisplayNotification());
        }
    }

    IEnumerator DisplayNotification() {
        _notificationWindow.gameObject.SetActive(false);
        if (_notificationsToDisplay.Count == 0) {
            _isNotificationDisplaying = false;
            Debug.Log("Siema");
            _notificationWindow.gameObject.SetActive(false);
            yield break;
        }
        Notification notification = _notificationsToDisplay.Dequeue();
        _notificationMessage.text = notification.message;
        _notificationMessage.gameObject.SetActive(true);
        _notificationsHistory.Add(notification);
        //UpdateLogs();
        _notificationWindow.transform.localScale = Vector3.zero;
        _notificationWindow.SetActive(true);
        LeanTween.scale(_notificationWindow, new Vector3(0.015f, 0.015f, 0.01f), 0.5f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(notification.displayTime);
        StartCoroutine(DisplayNotification());
    }

    public void RaiseTutorialNotification(TutorialNotification notification) {
        if (notification.messages.Count == 1) {
            _bigMessage.enabled = true;
            _tutorialNotification.SetActive(false);
            _bigMessage.text = notification.messages[0];
            return;
        }
        _messageGo1.SetActive(true);
        _messageGo2.SetActive(true);
        _messageGo3.SetActive(true);
        _tutorialNotification.transform.GetChild(1).GetComponent<TMP_Text>().text = "";

        _bigMessage.enabled = false;
        _tutorialNotification.SetActive(true);
        _messageGo1.GetComponent<TMP_Text>().text = notification.messages[0];
        _messageGo2.GetComponent<TMP_Text>().text = notification.messages[1];
        _messageGo3.GetComponent<TMP_Text>().text = notification.messages[2];
        LayoutRebuilder.ForceRebuildLayoutImmediate(_messageGo2.GetComponent<RectTransform>());
    }

    public void EndTutorial() {
        _tutorialNotification.SetActive(false);
        _bigMessage.enabled = false;
    }

    public void StartTutorial() {
        _tutorialNotification.SetActive(true);
        _bigMessage.enabled = true;
    }

    public void LoadPersistentData(GameData gameData) {
        _notificationsHistory = new List<Notification>(gameData.notificationGameData.notificationHistory);
    }

    public void SavePersistentData(ref GameData gameData) {
        gameData.notificationGameData.notificationHistory = _notificationsHistory;
    }

    /*void UpdateLogs() {
        for (int i = 0; i < _scrollContent.transform.childCount; i++) {
            Destroy(_scrollContent.transform.GetChild(i).gameObject);
        }
        int it = 0;
        foreach (Notification notification in _notificationsHistory) {
            GameObject go = Instantiate(_logPrerab, _scrollContent.transform);
            go.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = notification.message.ToString();
            if (it % 2 == 1) go.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
            it += 1;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollContent.GetComponent<RectTransform>());
    }*/
}