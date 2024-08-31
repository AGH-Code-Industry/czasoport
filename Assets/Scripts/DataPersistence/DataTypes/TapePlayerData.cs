using UnityEngine;

namespace DataPersistence.DataTypes {
    public class TapePlayerData : Data {
        [System.Serializable]
        public class TapePlayerSubData {
            public bool playingMusic;
        }

        public TapePlayerSubData data;

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<TapePlayerSubData>(inheritanceJson);
        }

    }
}