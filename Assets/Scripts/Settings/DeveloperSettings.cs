using CoinPackage.Debugging;
using Settings.Developer;
using UnityEngine;

namespace Settings {
    
    /// <summary>
    /// Manages different setting sets used for internal settings.
    /// </summary>
    public class DeveloperSettings : MonoBehaviour {
        public static DeveloperSettings Instance;

        public TimePlatformChangeSettingsSO tpcSettings;
        public ApplicationSettingsSO appSettings;
        
        private void Awake() {
            if (Instance == this) {
                CDebug.LogError($"{this} attempted to override DeveloperSettings singleton.", this);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}

