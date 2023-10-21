using CoinPackage.Debugging;
using UnityEngine;
using Utils;

namespace Items
{
    /// <summary>
    /// Scriptable object for items
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Definition/New Item")]
    
    public class ItemSO : ScriptableObject
    {
        [Header("Item Information")]
        public string itemName;
        public string description;
        public GameObject prefab;
        public Sprite image;
        
        [Header("Item Statistics")]
        public int durability;
        public float interactionDistance;
        public string pickUpNotification = "I say this when I'm picked up";
        
        public override string ToString() {
            return $"[Item, {itemName}]" % Colorize.Purple;
        }
    }
}