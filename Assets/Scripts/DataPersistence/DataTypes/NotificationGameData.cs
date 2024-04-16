using Notifications;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataPersistence.DataTypes {
    [System.Serializable]
    public class NotificationGameData {
        public List<Notification> notificationHistory;

        public NotificationGameData() {
            notificationHistory = new List<Notification>();
        }
    }
}