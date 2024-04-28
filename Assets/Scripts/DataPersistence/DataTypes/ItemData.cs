using Items;
using UnityEngine;

namespace DataPersistence.DataTypes {
    public class ItemData : Data {
        public ItemSO itemSo;
        public Vector3 position;
        public int durability;
        public string mapId;
    }
}