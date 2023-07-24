using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Developer {
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Settings/DeveloperSettings/ApplicationSettings")]
    public class ApplicationSettingsSO : ScriptableObject {
        [Tooltip("Name of the scene that loads global objects at the start of the application.")]
        public string globalDataSceneName;
    }
}