using System;
using Items;

namespace DataPersistence.DataTypes {
    [Serializable]
    public class InventoryItemData {
        public ItemSO itemSO;
        public int durability;
    }
}