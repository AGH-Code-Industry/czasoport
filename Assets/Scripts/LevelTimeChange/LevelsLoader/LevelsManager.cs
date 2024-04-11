using System;
using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using DataPersistence;
using CustomInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerScripts;
using Settings;

namespace LevelTimeChange.LevelsLoader {
    /// <summary>
    /// Manager of levels loading and switching system. Central point of communication between rest of
    /// included components. It is a singleton, attached to object present through the `game`.
    /// </summary>
    public class LevelsManager : MonoBehaviour, IDataPersistence {
        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        public static LevelsManager Instance { get; private set; }

        public LevelManager CurrentLevelManager => _currentLevelManager;

        /// <summary>
        /// All currently loaded levels.
        /// </summary>
        public Dictionary<LevelInfoSO, LevelManager> LoadedLevels;

        [SerializeField] private Animator animator;

        private LevelManager _currentLevelManager;
        private LevelInfoSO _currentLevel;
        private TimePlatformChangeSettingsSO _settings;
        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.LEVEL_SYSTEM];
        private bool _isFirstLevelLoading = true;

        private Transform _player;

        private void Awake() {
            if (Instance != null) {
                CDebug.LogError($"{this} tried to overwrite current singleton instance.", this);
                throw new SingletonOverrideException($"{this} tried to overwrite current singleton instance.");
            }

            Instance = this;
            LoadedLevels = new Dictionary<LevelInfoSO, LevelManager>();
            _settings = DeveloperSettings.Instance.tpcSettings;
        }

        private void Start() {
            _player = Player.Instance.GetComponent<Transform>();
        }

        /// <summary>
        /// Load first scene after joining the game
        /// </summary>
        public void LoadFirstLevel() {
            _logger.Log($"Loading {_currentLevel} as the current level.");
            _isFirstLevelLoading = true;
            SceneManager.LoadScene(_currentLevel.sceneName, LoadSceneMode.Additive);
            // Wait for discovery process from loading level and then do rest of the setup
        }

        /// <summary>
        /// Change active level.
        /// </summary>
        /// <param name="destinedLevelInfo">Level to be switched to.</param>
        /// <param name="destinationPortal">Portal to be switched to.</param>
        public void ChangeLevel(LevelInfoSO destinedLevelInfo, LevelPortal destinationPortal) {
            _logger.Log($"Changing level to {destinedLevelInfo}, destined portal: {destinationPortal}");

            // Order of actions in this function is crucial, do not change it unless
            // you know what you are doing
            // FOR REAL, I WROTE THIS, THEN CHANGED IT AND IT BROKE

            StartCoroutine(ChangeLevelAnim(destinedLevelInfo, destinationPortal));

            // var newLevel = LoadedLevels[destinedLevelInfo];
            // var oldLevel = _currentLevelManager;
            //
            //          newLevel.ActivateLevel();
            //          _currentLevelManager = newLevel;
            //          oldLevel.DeactivateLevel();
            //
            //          _player.position = destinationPortal.GetTeleportPoint(); // TODO: Change how we move the player
            //
            //          // oldLevel.DeactivateLevel();
            //
            // LoadLevels(newLevel);
            // UnloadLevels(newLevel);
        }

        private IEnumerator<WaitForSeconds> ChangeLevelAnim(LevelInfoSO destinedLevelInfo,
            LevelPortal destinationPortal) {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(_settings.platformChangeAnimLength / 4);
            var key = CInput.MovementLock.Lock();
            yield return new WaitForSeconds(_settings.platformChangeAnimLength / 4);

            var newLevel = LoadedLevels[destinedLevelInfo];
            var oldLevel = _currentLevelManager;

            newLevel.ActivateLevel();
            _currentLevelManager = newLevel;
            _currentLevel = destinedLevelInfo;
            oldLevel.DeactivateLevel();

            _player.position = destinationPortal.GetTeleportPoint(); // TODO: Change how we move the player

            // oldLevel.DeactivateLevel();

            LoadLevels(newLevel);
            UnloadLevels(newLevel);



            animator.SetTrigger("End");
            yield return new WaitForSeconds(_settings.platformChangeAnimLength / 4);
            CInput.MovementLock.Unlock(key);
            yield return new WaitForSeconds(_settings.platformChangeAnimLength / 4);
        }

        /// <summary>
        /// This method is used by the `LevelManager` of some scene to let the system know
        /// that the scene has been loaded and is ready for discovery process from current scene.
        /// </summary>
        /// <param name="level"></param>
        public void ReportForDiscovery(LevelInfoSO level) {
            if (_isFirstLevelLoading) { // Level reporting for discovery is the first loaded level
                _logger.Log(
                    $"Level {level} reported for discovery, {"omitted" % Colorize.Red} because it is starting level.");
                _logger.Log($"Finishing setup of the first level.");

                _currentLevelManager = LoadedLevels[_currentLevel];
                _currentLevelManager.ActivateLevel();
                _isFirstLevelLoading = false;
                _logger.Log($"Finished setup of the first level loading level.");

                LoadLevels(_currentLevelManager);
                return;
            }

            _logger.Log($"Level {level} reported for discovery, {"discovering" % Colorize.Green}.");
            _currentLevelManager.MakeDiscovery(level);
        }

        private void LoadLevel(LevelInfoSO level) {
            _logger.Log($"New scene is being loaded: {level}");
            SceneManager.LoadSceneAsync(level.sceneName, LoadSceneMode.Additive);
        }

        private void LoadLevels(LevelManager destinedLevel) {
            _logger.Log($"Loading additional scenes.");
            foreach (var level in destinedLevel.neighborLevels) {
                if (!LoadedLevels.ContainsKey(level)) {
                    LoadLevel(level);
                }
            }
        }

        private void UnLoadLevel(LevelInfoSO level) {
            _logger.Log($"Scene is being unloaded: {level}");
            SceneManager.UnloadSceneAsync(level.sceneName);
            LoadedLevels.Remove(level);
        }

        private void UnloadLevels(LevelManager levelInfo) {
            var scenesToRemove = new List<LevelInfoSO>();

            _logger.Log($"Unloading scenes.");
            foreach (LevelInfoSO level in LoadedLevels.Keys) {
                if (!levelInfo.neighborLevels.Contains(level) && levelInfo.currentLevel != level) {
                    scenesToRemove.Add(level);
                }
            }

            foreach (var scene in scenesToRemove) {
                UnLoadLevel(scene);
            }
        }

        public void LoadPersistentData(GameData gameData) {
            _currentLevel = Resources.Load<LevelInfoSO>(
                $"{DeveloperSettings.Instance.appSettings.lvlDefinitionsResPath}/{gameData.currentLevel}");
            _logger.Log($"Loaded from save: {_currentLevel}");
        }

        public void SavePersistentData(ref GameData gameData) {
            gameData.currentLevel = _currentLevel.sceneName;
        }

        public override string ToString() {
            return $"[LevelsManager]" % Colorize.Gold;
        }
    }
}