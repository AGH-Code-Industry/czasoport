using System.Collections;
using System.Collections.Generic;
using LevelTimeChange;
using LevelTimeChange.LevelsLoader;
using UnityEngine;

namespace DataPersistence {
    /// <summary>
    /// Settings for entire application that defines variables for startup, core processes and shutdown
    /// </summary>
    [CreateAssetMenu(fileName = "DefaultSaveData", menuName = "Settings/DefaultSaveData")]
    public class DefaultSaveDataSO : ScriptableObject {
        [Header("Main")]
        [Tooltip("Default starting scene.")]
        public LevelInfoSO startingLevel;
        [Tooltip("Default starting timeline.")]
        public TimeLine startingTimeline;

        [Header("Player")]
        [Tooltip("Starting player position (must be adjusted to timeline)")]
        public Vector2 startingPlayerPositionOffset;
    }
}