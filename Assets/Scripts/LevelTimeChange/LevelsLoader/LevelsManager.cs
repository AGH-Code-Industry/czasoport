using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTimeChange.LevelsLoader {
	public class LevelsManager : MonoBehaviour
	{
		public static LevelsManager Instance { get; private set; }
		
		[SerializeField] private LevelInfoSO startingLevel;
		
		private List<AvailableLevels> _loadedLevels = new();

		private void Awake() {
			if (Instance != null) {
				CDebug.LogError($"{this} tried to overwrite current singleton instance.", this);
			}
			Instance = this;

			SetUpStartLevel();
		}

		private void SetUpStartLevel() {
			StartCoroutine(LoadLevel(startingLevel.level, true));
			LoadLevels(startingLevel);
		}

		public void ChangeLevel(LevelInfoSO levelInfo) {
			LoadLevels(levelInfo);

			UnLoadLevels(levelInfo);
		}

		private IEnumerator LoadLevel(AvailableLevels level, bool visibility) {
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);

			while (!asyncLoad.isDone) {
				yield return null;
			}

			ToogleVisibilityOfScene(level, visibility);
			_loadedLevels.Add(level);
		}

		private void LoadLevels(LevelInfoSO levelInfo) {
			CDebug.Log("You are on " + levelInfo.level);
			ToogleVisibilityOfScene(levelInfo.level, true);
			foreach (AvailableLevels level in levelInfo.neighbourLevels) {
				if (!_loadedLevels.Contains(level)) {
					StartCoroutine(LoadLevel(level, false));
				}
				else {
					ToogleVisibilityOfScene(level, false);
				}
			}
		}

		private void UnLoadLevel(AvailableLevels level) {
			SceneManager.UnloadSceneAsync(level.ToString());
		}

		private void UnLoadLevels(LevelInfoSO levelInfo) {
			List<AvailableLevels> scenesToRemove = new List<AvailableLevels>();

			foreach (AvailableLevels level in _loadedLevels) {
				if (!levelInfo.neighbourLevels.Contains(level) && levelInfo.level != level) {
					UnLoadLevel(level);
					scenesToRemove.Add(level);
				}
			}

			foreach (AvailableLevels level in scenesToRemove) {
				_loadedLevels.Remove(level);
			}
		}

		private void ToogleVisibilityOfScene(AvailableLevels level, bool visibility) {
			Scene sceneToHide = SceneManager.GetSceneByName(level.ToString());
			if (sceneToHide.IsValid()) {
				GameObject[] rootObjects = sceneToHide.GetRootGameObjects();

				foreach (GameObject rootObject in rootObjects) {
					rootObject.SetActive(visibility);
				}
			}
		}
	}	
}
