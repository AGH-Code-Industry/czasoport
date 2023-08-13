using System;
using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTimeChange.LevelsLoader {
	public class LevelsManager : MonoBehaviour
	{
		public static LevelsManager Instance { get; private set; }

		public Dictionary<LevelInfoSO, LevelManager> LoadedLevelsDict;
		
		[SerializeField] private LevelInfoSO startingLevel; // TODO: This must be dynamic based on level save

		private LevelManager _currentLevelManager;
		
		//TODO: Remove this temporary abomination
		public Transform player;

		private void Awake() {
			if (Instance != null) {
				CDebug.LogError($"{this} tried to overwrite current singleton instance.", this);
			}
			Instance = this;

			//_loadedLevels = new List<LevelInfoSO>();
			LoadedLevelsDict = new Dictionary<LevelInfoSO, LevelManager>();

			SetUpStartLevel();
		}

		private void Start() {
			LoadedLevelsDict.Add(startingLevel, GetLevelManager(startingLevel));
			_currentLevelManager = LoadedLevelsDict[startingLevel];
			_currentLevelManager.ActivateLevel();
		}

		private void SetUpStartLevel() {
			SceneManager.LoadScene(startingLevel.sceneName, LoadSceneMode.Additive);
			LoadLevels(startingLevel);
		}

		public void ChangeLevel(LevelInfoSO destinedLevelInfo, LevelPortal sourcePortal) {
			var newLevelManager = LoadedLevelsDict[destinedLevelInfo];
			
			_currentLevelManager.DeactivateLevel();
			newLevelManager.ActivateLevel();
			_currentLevelManager = newLevelManager;
			
			// TODO: Change how we move the player
			player.position = sourcePortal.GetTeleportPoint();
			
			LoadLevels(destinedLevelInfo);
			_currentLevelManager.MakeDiscovery(); // WARNING: This will not ensure that discovery is performed after scenes have loaded.
			UnloadLevels(destinedLevelInfo);
		}

		private IEnumerator LoadLevel(LevelInfoSO level) {
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level.sceneName, LoadSceneMode.Additive);

			while (!asyncLoad.isDone) {
				yield return null;
			}
			
			LoadedLevelsDict.Add(level, GetLevelManager(level));
		}

		private void LoadLevels(LevelInfoSO destinedLevel) {
			foreach (var level in destinedLevel.neighbourLevels) {
				if (!LoadedLevelsDict.ContainsKey(level)) {
					Coroutine asyncOperation = StartCoroutine(LoadLevel(level));
				}
			}
		}

		private void UnLoadLevel(LevelInfoSO level) {
			SceneManager.UnloadSceneAsync(level.sceneName);
			LoadedLevelsDict.Remove(level);
		}

		private void UnloadLevels(LevelInfoSO levelInfo) {
			foreach (LevelInfoSO level in LoadedLevelsDict.Keys) {
				if (!levelInfo.neighbourLevels.Contains(level) && levelInfo != level) {
					UnLoadLevel(level);
				}
			}
		}

		private LevelManager GetLevelManager(LevelInfoSO levelInfo) {
			// TODO: Exception when LevelManager could not be loaded
			var x = SceneManager.GetSceneByName(levelInfo.sceneName)
				.GetRootGameObjects();
			foreach (var xc in x) {
				CDebug.Log(xc);
			}
			var newLevelManager = SceneManager.GetSceneByName(levelInfo.sceneName)
				.GetRootGameObjects()[0]
				.GetComponent<LevelManager>();
			return newLevelManager;
		}
	}	
}
