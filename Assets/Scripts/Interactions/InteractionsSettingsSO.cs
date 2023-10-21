using UnityEngine;

namespace Interactions {
    [CreateAssetMenu(menuName = "Settings/InteractionsSettings", fileName = "InteractionsSettings")]
    public class InteractionsSettingsSO : ScriptableObject {
        [Tooltip("What layer name is used to find interactable items on map.")]
        public string interactablesLayer = "Interactables";
        public string itemsLayer = "Items";

        [Tooltip("Radius of normal interactions.")]
        [Range(0.01f, 3f)]
        public float defaultInteractionRadius = 5.0f;

        [Tooltip("Time between checking for interactable items around the player.")]
        [Range(0.01f, 0.5f)]
        public float interactionCheckInterval = 0.1f;
    }
}