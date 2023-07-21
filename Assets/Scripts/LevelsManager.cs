using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
	public static LevelsManager Instance { get; private set; }
	
	[SerializeField] private LevelInfoSO startingLevel;

	private List<LevelsList> loadedLevels = new();

	private void Start() {

	}

	private void Awake() {
		if (Instance != null) {
			CDebug.LogError("More than one Scene Manager in Main Scene");
		}
		Instance = this;

		LoadLevel(startingLevel.level);
		LoadLevels(startingLevel);
	}

	public void LoadLevel(LevelsList level) {
		SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
		loadedLevels.Add(level);
	}

	public void LoadLevels(LevelInfoSO levelInfo) {
		CDebug.Log("You are on " + levelInfo.level.ToString());
		foreach (LevelsList scene in levelInfo.neighbourLevels) {
			if (!loadedLevels.Contains(scene)) {
				LoadLevel(scene);
			}
		}

		UnLoadLevels(levelInfo);
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
}
