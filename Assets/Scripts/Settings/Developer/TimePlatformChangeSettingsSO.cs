using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Developer {
    
    /// <summary>
    /// Settings for scene changing and time changing systems.
    /// </summary>
    [CreateAssetMenu(fileName = "TimePlatformChangeSettings", menuName = "Settings/DeveloperSettings/TimePlatformChangeSettings")]
    public class TimePlatformChangeSettingsSO : ScriptableObject {
        [Header("Time Change Settings")]
        [Tooltip("By what offset should Past and Future platforms' transform differ from Present platform.")]
        public Vector2 offsetFromPresentPlatform;
    }
}
