using System;
using InteractableObjectSystem.Objects;
using UnityEngine;

namespace DataPersistence.DataTypes {
    public class InteractableData : Data {
        [Serializable]
        public class InteractableSubData {
            public int state;
        }

        public InteractableSubData data;

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<InteractableSubData>(inheritanceJson);
        }
    }

}