using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    [Serializable]
    public struct SerializableGuid: IComparable, IComparable<SerializableGuid>, IEquatable<SerializableGuid>
    {
        public string value;

        private SerializableGuid(string guid) {
            value = guid;
        }

        public static implicit operator SerializableGuid(Guid guid) {
            return new SerializableGuid(guid.ToString());
        }

        public static implicit operator Guid(SerializableGuid serializableGuid) {
            return new Guid(serializableGuid.value);
        }

        public int CompareTo(object other) {
            if (other == null)
                return 1;
            if (other is not SerializableGuid guid)
                throw new ArgumentException("Must be SerializableGuid");
            return guid.value == value ? 0 : 1;
        }

        public int CompareTo(SerializableGuid other) {
            return other.value == value ? 0 : 1;
        }

        public bool Equals(SerializableGuid other) {
            return value == other.value;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return (value != null ? value.GetHashCode() : 0);
        }

        public override string ToString() {
            return (value != null ? new Guid(value).ToString() : string.Empty);
        }
    }
}
