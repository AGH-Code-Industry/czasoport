using LevelTimeChange;
using Settings;
using UnityEngine;

namespace DataPersistence.DataTypes {
    [System.Serializable]
    public class PlayerGameData {
        public Vector2 position;

        public PlayerGameData() {
            position = DeveloperSettings.Instance.dsdSettings.startingPlayerPosition;
        }
    }
}