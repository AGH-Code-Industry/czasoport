using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using Settings;
using Unity.VisualScripting;
using UnityEngine;

namespace DataPersistence {
    public class DataPersistenceManager : MonoBehaviour {
        public static DataPersistenceManager Instance { get; private set; }

        [NonSerialized]
        public GameData gameData;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.DATA_PERSISTENCE];
        private FileDataHandler _fileDataHandler;
        private ApplicationSettingsSO _settings;

        private void Awake() {
            if (Instance != null) {
                _logger.LogError($"{this} tried to overwrite current singleton instance.", this);
                throw new SingletonOverrideException($"{this} tried to overwrite current singleton instance.");
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _settings = DeveloperSettings.Instance.appSettings;
            _fileDataHandler = new FileDataHandler(UnityEngine.Application.dataPath, _settings.saveFileName);
        }

        public void CreateNewGame() {
            _logger.Log($"Creating new save game.");
            gameData = new GameData();
        }

        public void LoadGameFromDisk() {
            _logger.Log($"Loading game data from disk.");
            gameData = _fileDataHandler.Load();
        }

        public void SaveGameToDisk() {
            _logger.Log($"Saving game data to disk.");
            _fileDataHandler.Save(gameData);
        }

        public void LoadGame() {
            _logger.Log($"Loading game data into objects.");
            var persistentObjects = FindPersistentObjects();
            foreach (var persistentObject in persistentObjects) {
                persistentObject.LoadPersistentData(gameData);
            }
        }

        public void SaveGame() {
            _logger.Log($"Saving game data from objects.");
            var persistentObjects = FindPersistentObjects();
            foreach (var persistentObject in persistentObjects) {
                _logger.Log($"Saving data for {persistentObject}");
                persistentObject.SavePersistentData(ref gameData);
            }
            SaveGameToDisk();
        }

        private List<IDataPersistence> FindPersistentObjects() {
            IEnumerable<IDataPersistence> persistentObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
            return new List<IDataPersistence>(persistentObjects);
        }
    }
}