using System;
using Items;
using UnityEngine;

namespace DataPersistence.DataTypes {
    public class PedestalData : Data {

        [Serializable]
        public class PedestalSubData
        {
            public ItemSO currentRock;
        }

        public PedestalSubData data;

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<PedestalSubData>(inheritanceJson);
        }
    }
}