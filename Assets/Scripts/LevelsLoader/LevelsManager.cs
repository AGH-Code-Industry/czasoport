using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelsManager : MonoBehaviour
{
	public static LevelsManager Instance { get; private set; }
	
	[SerializeField] private LevelInfoSO startingLevel;

	private List<LevelsList> loadedLevels = new();

	private void Awake() {
		if (Instance != null) {
			CDebug.LogError("More than one Scene Manager in Main Scene");
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

	private IEnumerator LoadLevel(LevelsList level, bool visibility) {
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);

		while (!asyncLoad.isDone) {
			yield return null;
		}

		ToogleVisibilityOfScene(level, visibility);
		loadedLevels.Add(level);
	}

	private void LoadLevels(LevelInfoSO levelInfo) {
		CDebug.Log("You are on " + levelInfo.level.ToString());
		ToogleVisibilityOfScene(levelInfo.level, true);
		foreach (LevelsList level in levelInfo.neighbourLevels) {
			if (!loadedLevels.Contains(level)) {
				StartCoroutine(LoadLevel(level, false));
			}
			else {
				ToogleVisibilityOfScene(level, false);
			}
		}
	}

	private void UnLoadLevel(LevelsList level) {
		SceneManager.UnloadSceneAsync(level.ToString());
	}

	private void UnLoadLevels(LevelInfoSO levelInfo) {
		List<LevelsList> scenesToRemove = new List<LevelsList>();

		foreach (LevelsList level in loadedLevels) {
			if (!levelInfo.neighbourLevels.Contains(level) && levelInfo.level != level) {
				UnLoadLevel(level);
				scenesToRemove.Add(level);
			}
		}

		foreach (LevelsList level in scenesToRemove) {
			loadedLevels.Remove(level);
		}
	}

	private void ToogleVisibilityOfScene(LevelsList level, bool visibility) {
		Scene sceneToHide = SceneManager.GetSceneByName(level.ToString());
		if (sceneToHide.IsValid()) {
			GameObject[] rootObjects = sceneToHide.GetRootGameObjects();

			foreach (GameObject rootObject in rootObjects) {
				rootObject.SetActive(visibility);
			}
		}
	}
}
