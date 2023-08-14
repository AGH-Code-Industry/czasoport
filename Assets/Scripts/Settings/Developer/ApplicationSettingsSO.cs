using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Developer {
    /// <summary>
    /// Settings for entire application that defines variables for startup, core processes and shutdown
    /// </summary>
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Settings/DeveloperSettings/ApplicationSettings")]
    public class ApplicationSettingsSO : ScriptableObject {
        [Tooltip("Name of the scene that loads global objects at the start of the application.")]
        public string globalDataSceneName;

        [Tooltip("Name of the scene to load when press Play button on menu.")]
        public string sceneToLoadName;
    }
}