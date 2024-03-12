using System;
using Application;
using CoinPackage.Debugging;
using DataPersistence;
using Interactions;
using InventorySystem;
using LevelTimeChange;
using PlayerScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings {
    
    /// <summary>
    /// Manages different setting sets used for internal settings.
    /// </summary>
    public class DeveloperSettings : MonoBehaviour {
        public static DeveloperSettings Instance;

        /// <summary>
        /// Settings for Time Change and Platform Change systems
        /// </summary>
        public TimePlatformChangeSettingsSO tpcSettings;
        
        /// <summary>
        /// Player settings
        /// </summary>
        public PlayerSettingsSO playerSettings;
        
        /// <summary>
        /// Inventory settings
        /// </summary>
        public InventorySettingsSO invSettings;
        
        /// <summary>
        /// Interactions settings
        /// </summary>
        public InteractionsSettingsSO intSettings;
        
        /// <summary>
        /// Application settings
        /// </summary>
        public ApplicationSettingsSO appSettings;

        /// <summary>
        /// Default settings for new game save
        /// </summary>
        public DefaultSaveDataSO dsdSettings;
        
        private void Awake() {
            if (Instance == this || Instance != null) {
                CDebug.LogError($"{this} attempted to override DeveloperSettings singleton.", this);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}

