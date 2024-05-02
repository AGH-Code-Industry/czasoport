using System;
using InteractableObjectSystem.Objects;
using UnityEngine;

namespace DataPersistence.DataTypes {
    public class DoorData : Data {
        [Serializable]
        public class DoorSubData {
            public LockedDoor.DoorState doorState;
        }

        public DoorSubData data;

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<DoorSubData>(inheritanceJson);
        }
    }
}