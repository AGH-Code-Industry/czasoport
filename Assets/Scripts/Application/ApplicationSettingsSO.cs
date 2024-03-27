using System.Collections;
using System.Collections.Generic;
using LevelTimeChange.LevelsLoader;
using UnityEngine;

namespace Application {
    /// <summary>
    /// Settings for entire application that defines variables for startup, core processes and shutdown
    /// </summary>
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Settings/ApplicationSettings")]
    public class ApplicationSettingsSO : ScriptableObject {
        [Header("Scene names")]
        [Tooltip("Name of the scene to load when press Play button on menu.")]
        public string gameSceneName;

        [Tooltip("Name of the scene to load when press Play button on menu.")]
        public string sceneToLoadName;

        [Tooltip("Time that the notificatino will be desplaying on screen")]
        public float notificationDuration;
        [Header("Resource paths")]
        [Tooltip("Path to level definitions")]
        public string lvlDefinitionsResPath;

        [Header("Data persistence")]
        [Tooltip("Default filename for data save.")]
        public string saveFileName = "save.game";
    }
}