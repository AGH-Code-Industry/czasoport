using CoinPackage.Debugging;
using Notifications;
using UnityEngine;
using Utils;

namespace InteractableObjectSystem {
    /// <summary>
    /// Scriptable object for items
    /// </summary>
    [CreateAssetMenu(fileName = "New Interactable", menuName = "Definition/New Interactable")]

    public class InterObjectDefinition : ScriptableObject {
        [Header("Interactable Information")]
        public string interactableName;
        public string description;
        public GameObject prefab;

        [Header("Generic Notifications")]
        public Notification failedHandInterNotification;
        public Notification successfulHandInterNotification;
        public Notification failedItemInterNotification;
        public Notification successfulItemInterNotification;

        public override string ToString() {
            return $"[Interactable, {interactableName}]" % Colorize.Purple;
        }
    }
}