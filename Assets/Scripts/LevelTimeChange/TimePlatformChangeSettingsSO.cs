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
        [Tooltip("Length of the timeline change animation")]
        [Range(0f, 1f)]
        public float timelineChangeAnimLength = 0.5f;
        [Tooltip("Length of the platform change animation")]
        [Range(0f, 1f)]
        public float platformChangeAnimLength = 0.5f;

        [Tooltip("By what offset should Past and Future platforms' transform differ from Present platform.")]
        public Vector2 offsetFromPresentPlatform;

        [Tooltip("Whether you can move from past to future and vice versa.")]
        public bool loopTimeChange;
    }
}