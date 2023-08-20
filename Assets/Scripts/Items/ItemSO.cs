using UnityEngine;

namespace Items
{
    /// <summary>
    /// Scriptable object for items
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
    
    public class ItemSO : ScriptableObject
    {
        public int id;
        public Sprite image;
        public string itemName;
        public string description;
        public int durability;
        public float interactionDistance;
    }
}