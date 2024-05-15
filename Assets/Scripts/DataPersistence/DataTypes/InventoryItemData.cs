using System;
using Items;
using Utils;

namespace DataPersistence.DataTypes {
    [Serializable]
    public class InventoryItemData {
        public ItemSO itemSO;
        public int durability;
        public SerializableGuid id;
    }
}