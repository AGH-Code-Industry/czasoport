using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using CoinPackage.Debugging;
using LevelTimeChange.LevelsLoader;
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
            var persistentObjects = FindPersistentObjects(false);
            foreach (var persistentObject in persistentObjects) {
                persistentObject.LoadPersistentData(gameData);
            }
        }

        public void SaveGame() {
            LevelsManager.Instance.ActivateContentAll();
            _logger.Log($"Saving game data from objects.");
            var persistentObjects = FindPersistentObjects(false);
            persistentObjects.AddRange(FindPersistentObjects(true));
            foreach (var persistentObject in persistentObjects) {
                _logger.Log($"Saving data for {persistentObject}");
                persistentObject.SavePersistentData(ref gameData);
            }
            SaveGameToDisk();
            LevelsManager.Instance.DeactivateContentNotCurrent();
        }

        public void LoadSceneObjects() {
            var persistentObjects = FindPersistentObjects(true);
            foreach (var persistentObject in persistentObjects) {
                persistentObject.LoadPersistentData(gameData);
            }
        }

        private List<IDataPersistence> FindPersistentObjects(bool sceneObjects) {
            var persistentObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().Where(obj => obj.SceneObject == sceneObjects);
            return new List<IDataPersistence>(persistentObjects);
        }
    }
}