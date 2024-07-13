using DataPersistence.DataTypes;
using LevelTimeChange;
using Notifications;
using Settings;
using System.Collections.Generic;

namespace DataPersistence {
    [System.Serializable]
    public class GameData {
        public PlayerGameData playerGameData;
        public TimeLine currentTimeline;
        public string currentLevel;
        public NotificationGameData notificationGameData;

        public GameData() {
            playerGameData = new PlayerGameData();
            notificationGameData = new NotificationGameData();
            currentTimeline = DeveloperSettings.Instance.dsdSettings.startingTimeline;
            currentLevel = DeveloperSettings.Instance.dsdSettings.startingLevel.sceneName;
        }
    }
}