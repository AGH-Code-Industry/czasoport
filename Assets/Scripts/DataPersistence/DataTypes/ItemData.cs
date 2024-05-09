using System;
using Items;
using UnityEngine;

namespace DataPersistence.DataTypes {
    public class ItemData : Data {
        [Serializable]
        public class ItemSubData {
            public ItemSO itemSo;
            public Vector3 position;
            public int durability;
            public string mapId;
            public bool hidden;
        }

        public ItemSubData data = new();

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<ItemSubData>(inheritanceJson);
        }
    }
}