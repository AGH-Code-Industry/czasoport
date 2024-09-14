using System;
using DataPersistence.DataTypes;
using LevelTimeChange;
using Notifications;
using Settings;
using System.Collections.Generic;
using System.Linq;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace DataPersistence {
    [System.Serializable]
    public class GameData {
        public PlayerGameData playerGameData;
        public TimeLine currentTimeline;
        public string currentLevel;
        public NotificationGameData notificationGameData;
        public List<SerializableGuid> itemHideout;
        public bool tutorialFinished = false;
        public int tutorialStage = 0;

        [SerializeField] private List<Data> objectDatas;
        [SerializeField] private List<string> savedLevels;

        public GameData() {
            playerGameData = new PlayerGameData();
            notificationGameData = new NotificationGameData();
            currentTimeline = DeveloperSettings.Instance.dsdSettings.startingTimeline;
            currentLevel = DeveloperSettings.Instance.dsdSettings.startingLevel.sceneName;
            objectDatas = new List<Data>();
            savedLevels = new List<string>();
            itemHideout = new List<SerializableGuid>();
        }

        public void SetObjectData(Data data) {
            if (string.IsNullOrEmpty(data.id.value) || data.id == Guid.Empty) {
                CDebug.LogWarning("Tried saving an object data with an empty ID!");
                return;
            }

            if (objectDatas.Any(obj => obj.id.Equals(data.id))) {
                RemoveObjectData(data.id);
            }

            objectDatas.Add(data);
        }

        public T GetObjectData<T>(SerializableGuid id) where T : Data, new() {
            if (!objectDatas.Any(obj => obj.id.Equals(id))) {
                CDebug.LogException(
                    new System.Exception(
                        $"Tried getting an object data with the ID {id.ToString()} that does not exist!"));
                return null;
            }

            var obj = objectDatas.FirstOrDefault(obj => obj.id.Equals(id));
            return obj?.Deserialize<T>();
        }

        public bool ContainsObjectData(SerializableGuid id) {
            return objectDatas.Any(obj => obj.id.Equals(id));
        }

        public void RemoveObjectData(SerializableGuid id) {
            if (!ContainsObjectData(id))
                return;

            objectDatas.Remove(objectDatas.First(obj => obj.id.Equals(id)));
        }

        public IEnumerable<Data> GetObjectDatas() {
            return objectDatas;
        }

        public void MarkLevelSaved(string levelId) {
            if (savedLevels.Contains(levelId))
                return;

            savedLevels.Add(levelId);
        }

        public bool IsLevelSaved(string levelId) {
            return savedLevels.Contains(levelId);
        }
    }
}