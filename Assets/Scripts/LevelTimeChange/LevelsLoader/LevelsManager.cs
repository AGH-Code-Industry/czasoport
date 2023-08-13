using System;
using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTimeChange.LevelsLoader {
	public class LevelsManager : MonoBehaviour
	{
		public static LevelsManager Instance { get; private set; }

		public Dictionary<LevelInfoSO, LevelManager> LoadedLevelsDict;
		
		[SerializeField] private LevelInfoSO startingLevel; // TODO: This must be dynamic based on level save

		private LevelManager _currentLevelManager;
		private readonly CLogger _logger = Loggers.LoggersList["LEVEL_SYSTEM"];
		private bool _isLoading = true;
		
		//TODO: Remove this temporary abomination
		public Transform player;

		private void Awake() {
			if (Instance != null) {
				CDebug.LogError($"{this} tried to overwrite current singleton instance.", this);
			}
			Instance = this;
			
			LoadedLevelsDict = new Dictionary<LevelInfoSO, LevelManager>();
			
			_logger.Log("Loading starting scene.");
			SceneManager.LoadScene(startingLevel.sceneName, LoadSceneMode.Additive);
		}

		private void Start() {
			_logger.Log("Activating starting scene.");
			_currentLevelManager = LoadedLevelsDict[startingLevel];
			_currentLevelManager.ActivateLevel();
			_isLoading = false;
			LoadLevels(startingLevel);
		}

		public void ChangeLevel(LevelInfoSO destinedLevelInfo, LevelPortal destinationPortal) {
			_logger.Log($"Changing scene, destination: {destinedLevelInfo.sceneName}");
			
			var newLevel = LoadedLevelsDict[destinedLevelInfo];
			var oldLevel = _currentLevelManager;
			
			newLevel.ActivateLevel();
			_currentLevelManager = newLevel;
			player.position = destinationPortal.GetTeleportPoint(); // TODO: Change how we move the player
			
			oldLevel.DeactivateLevel();

			LoadLevels(destinedLevelInfo);
			UnloadLevels(destinedLevelInfo);
		}

		public void ReportForDiscovery(LevelInfoSO level) {
			if (_isLoading) {
				_logger.Log($"Level {level.sceneName} reported for discovery, {"omitted" % Colorize.Red}.");
				return;
			}
			_logger.Log($"Level {level.sceneName} reported for discovery, {"discovering" % Colorize.Green}.");
			_currentLevelManager.MakeDiscovery(level);
		}

		private void LoadLevel(LevelInfoSO level) { 
			_logger.Log($"New scene is being loaded: {level.sceneName % Colorize.Magenta}");
			SceneManager.LoadSceneAsync(level.sceneName, LoadSceneMode.Additive);
		}

		private void LoadLevels(LevelInfoSO destinedLevel) {
			_logger.Log($"Loading new scenes.");
			foreach (var level in destinedLevel.neighbourLevels) {
				if (!LoadedLevelsDict.ContainsKey(level)) {
					LoadLevel(level);
				}
			}
		}

		private void UnLoadLevel(LevelInfoSO level) {
			_logger.Log($"Scene is being unloaded: {level.sceneName % Colorize.Magenta}");
			SceneManager.UnloadSceneAsync(level.sceneName);
			LoadedLevelsDict.Remove(level);
		}

		private void UnloadLevels(LevelInfoSO levelInfo) {
			var scenesToRemove = new List<LevelInfoSO>();
			
			_logger.Log($"Unloading scenes.");
			foreach (LevelInfoSO level in LoadedLevelsDict.Keys) {
				if (!levelInfo.neighbourLevels.Contains(level) && levelInfo != level) {
					scenesToRemove.Add(level);
				}
			}
			foreach (var scene in scenesToRemove) {
				UnLoadLevel(scene);
			}
		}

		private LevelManager GetLevelManager(LevelInfoSO levelInfo) {
			// TODO: Exception when LevelManager could not be loaded
			var newLevelManager = SceneManager.GetSceneByName(levelInfo.sceneName)
				.GetRootGameObjects()[0]
				.GetComponent<LevelManager>();
			return newLevelManager;
		}
	}	
}
