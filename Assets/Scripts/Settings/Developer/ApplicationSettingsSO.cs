using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Developer {
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Settings/DeveloperSettings/ApplicationSettings")]
    // Settings for entire application that defines variables for startup and core processes
    public class ApplicationSettingsSO : ScriptableObject {
        [Tooltip("Name of the scene that loads global objects at the start of the application.")]
        public string globalDataSceneName;
    }
}