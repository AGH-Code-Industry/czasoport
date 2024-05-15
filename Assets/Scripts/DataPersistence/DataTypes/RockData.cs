using System;
using InteractableObjectSystem.Objects;
using UnityEngine;

namespace DataPersistence.DataTypes {
    [Serializable]
    public class RockData : Data {
        [Serializable]
        public class RockSubData {
            public int state;
        }

        public RockSubData data;

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<RockSubData>(inheritanceJson);
        }
    }
}