using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTimeChange {
    
    /// <summary>
    /// Settings for scene changing and time changing systems.
    /// </summary>
    [CreateAssetMenu(fileName = "TimePlatformChangeSettings", menuName = "Settings/TimePlatformChangeSettings")]
    public class TimePlatformChangeSettingsSO : ScriptableObject {
        
        [Header("Time Change Settings")]
        
        [Tooltip("By what offset should Past and Future platforms' transform differ from Present platform.")]
        public Vector2 offsetFromPresentPlatform;
        
        [Tooltip("Do you want to go back to Past by trying to go forward in TimeLine by being in Future (and vice versa)?")]
        public bool loopTimeChange;
    }
}
