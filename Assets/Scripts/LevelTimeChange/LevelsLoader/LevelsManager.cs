using System;
using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.GlobalExceptions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTimeChange.LevelsLoader {
	/// <summary>
	/// Manager of levels loading and switching system. Central point of communication between rest of
	/// included components. It is a singleton, attached to object present through the `game`.
	/// </summary>
	public class LevelsManager : MonoBehaviour
	{
		/// <summary>
		/// Singleton instance of this class.
		/// </summary>
		public static LevelsManager Instance { get; private set; }

        public LevelManager CurrentLevelManager => _currentLevelManager;

		/// <summary>
		/// All currently loaded levels.
		/// </summary>
		public Dictionary<LevelInfoSO, LevelManager> LoadedLevels;
		
		[SerializeField] private LevelInfoSO startingLevel; // TODO: This must be dynamic based on level save

		private LevelManager _currentLevelManager;
		private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.LEVEL_SYSTEM];
		private bool _isLoading = true;
		
		//TODO: Remove this temporary abomination
		public Transform player;

		private void Awake() {
			if (Instance != null) {
				CDebug.LogError($"{this} tried to overwrite current singleton instance.", this);
				throw new SingletonOverrideException($"{this} tried to overwrite current singleton instance.");
			}
			Instance = this;
			
			LoadedLevels = new Dictionary<LevelInfoSO, LevelManager>();
			
			_logger.Log("Loading starting scene.");
			SceneManager.LoadScene(startingLevel.sceneName, LoadSceneMode.Additive);
		}

		private void Start() {
			_logger.Log("Activating starting scene.");
			_currentLevelManager = LoadedLevels[startingLevel];
			_currentLevelManager.ActivateLevel();
			_isLoading = false;
			LoadLevels(startingLevel);
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
			var newLevel = LoadedLevels[destinedLevelInfo];
			var oldLevel = _currentLevelManager;

            newLevel.ActivateLevel();
            _currentLevelManager = newLevel;
            oldLevel.DeactivateLevel();

            player.position = destinationPortal.GetTeleportPoint(); // TODO: Change how we move the player

            // oldLevel.DeactivateLevel();

			LoadLevels(destinedLevelInfo);
			UnloadLevels(destinedLevelInfo);
		}

		/// <summary>
		/// This method is used by the `LevelManager` of some scene to let the system know
		/// that the scene has been loaded and is ready for discovery process from current scene.
		/// </summary>
		/// <param name="level"></param>
		public void ReportForDiscovery(LevelInfoSO level) {
			if (_isLoading) {
				_logger.Log($"Level {level} reported for discovery, {"omitted" % Colorize.Red}.");
				return;
			}
			_logger.Log($"Level {level} reported for discovery, {"discovering" % Colorize.Green}.");
			_currentLevelManager.MakeDiscovery(level);
		}

		private void LoadLevel(LevelInfoSO level) { 
			_logger.Log($"New scene is being loaded: {level}");
			SceneManager.LoadSceneAsync(level.sceneName, LoadSceneMode.Additive);
		}

		private void LoadLevels(LevelInfoSO destinedLevel) {
			_logger.Log($"Loading new scenes.");
			foreach (var level in destinedLevel.neighbourLevels) {
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

		private void UnloadLevels(LevelInfoSO levelInfo) {
			var scenesToRemove = new List<LevelInfoSO>();
			
			_logger.Log($"Unloading scenes.");
			foreach (LevelInfoSO level in LoadedLevels.Keys) {
				if (!levelInfo.neighbourLevels.Contains(level) && levelInfo != level) {
					scenesToRemove.Add(level);
				}
			}
			foreach (var scene in scenesToRemove) {
				UnLoadLevel(scene);
			}
		}
	}	
}
