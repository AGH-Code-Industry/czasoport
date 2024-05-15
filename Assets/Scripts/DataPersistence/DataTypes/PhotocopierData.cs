using UnityEngine;

namespace DataPersistence.DataTypes {
    public class PhotocopierData : Data {
        public class PhotocopierSubData {
            public int stage;
            public bool itemTaken;
        }

        public PhotocopierSubData data;

        public override void SerializeInheritance() {
            inheritanceJson = JsonUtility.ToJson(data);
        }

        public override void DeserializeInheritance() {
            data = JsonUtility.FromJson<PhotocopierSubData>(inheritanceJson);
        }

    }
}