using DataPersistence.DataTypes;
using LevelTimeChange;
using Notifications;
using Settings;
using System.Collections.Generic;
using System.Linq;
using CoinPackage.Debugging;
using Utils;

namespace DataPersistence {
    [System.Serializable]
    public class GameData {
        public PlayerGameData playerGameData;
        public TimeLine currentTimeline;
        public string currentLevel;
        public NotificationGameData notificationGameData;

        private List<Data> _objectDatas;

        public GameData() {
            playerGameData = new PlayerGameData();
            notificationGameData = new NotificationGameData();
            currentTimeline = DeveloperSettings.Instance.dsdSettings.startingTimeline;
            currentLevel = DeveloperSettings.Instance.dsdSettings.startingLevel.sceneName;
            _objectDatas = new List<Data>();
        }

        public void AddObjectData(Data data) {
            if (_objectDatas.Any(obj => obj.id.Equals(data.id))) {
                CDebug.LogWarning($"Tried adding an object data with the ID {data.id.ToString()} that already exists!");
                return;
            }

            _objectDatas.Add(data);
        }

        public T GetObjectData<T>(SerializableGuid id) where T : Data {
            if (!_objectDatas.Any(obj => obj.id.Equals(id))) {
                CDebug.LogException(new System.Exception($"Tried getting an object data with the ID {id.ToString()} that does not exist!"));
                return null;
            }

            return (T) _objectDatas.FirstOrDefault(obj => obj.id.Equals(id));
        }

        public bool ContainsObjectData(SerializableGuid id) {
            return _objectDatas.Any(obj => obj.id.Equals(id));
        }
    }
}