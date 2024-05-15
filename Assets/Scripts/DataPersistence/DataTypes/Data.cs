using System;
using Utils;

namespace DataPersistence.DataTypes {
    [Serializable]
    public class Data {
        public SerializableGuid id;
        public string inheritanceJson;

        public virtual void SerializeInheritance() {
            inheritanceJson = "{}";
        }

        public virtual void DeserializeInheritance() {
        }

        public T Deserialize<T>() where T : Data, new() {
            var ret = new T {
                id = id,
                inheritanceJson = inheritanceJson
            };
            ret.DeserializeInheritance();
            return ret;
        }
    }
}