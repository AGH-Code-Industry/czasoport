using LevelTimeChange;
using Settings;
using UnityEngine;

namespace DataPersistence.DataTypes {
    [System.Serializable]
    public class PlayerGameData {
        public Vector2 position;

        public PlayerGameData() {
            // Based on default timeline, move player to correct position on that timeline
            position = DeveloperSettings.Instance.dsdSettings.startingPlayerPositionOffset;
            if (DeveloperSettings.Instance.dsdSettings.startingTimeline == TimeLine.Future) {
                position += DeveloperSettings.Instance.tpcSettings.offsetFromPresentPlatform;
            }
            else if (DeveloperSettings.Instance.dsdSettings.startingTimeline == TimeLine.Past) {
                position -= DeveloperSettings.Instance.tpcSettings.offsetFromPresentPlatform;
            }
        }
    }
}