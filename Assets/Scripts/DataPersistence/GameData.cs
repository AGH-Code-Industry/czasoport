using DataPersistence.DataTypes;
using LevelTimeChange;
using Settings;

namespace DataPersistence {
    [System.Serializable]
    public class GameData {
        public PlayerGameData playerGameData;
        public TimeLine currentTimeline;
        public string currentLevel;

        public GameData() {
            playerGameData = new PlayerGameData();
            currentTimeline = DeveloperSettings.Instance.dsdSettings.startingTimeline;
            currentLevel = DeveloperSettings.Instance.dsdSettings.startingLevel.sceneName;
        }
    }
}