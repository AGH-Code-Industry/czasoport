using UnityEngine;
using Utils.Attributes;

namespace Utils {
    public class UniqueScriptableObject : ScriptableObject {
        [Tooltip("Unique object identifier")]
        [ObjectIdentifier]
        public string uniqueId;

        [ContextMenu("Generate new UniqueId")]
        public void GenerateUniqueId() {
            uniqueId = System.Guid.NewGuid().ToString();
        }
    }
}