using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    [CreateAssetMenu(menuName = "Settings/SystemsSettings/InventorySettings", fileName = "InventorySettings")]
    public class InventorySettings : ScriptableObject {
        [Tooltip("How many slots is available in inventory.")]
        public int itemsCount = 6;
    }
}
