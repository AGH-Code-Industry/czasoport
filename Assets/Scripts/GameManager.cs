using CoinPackage.Debugging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private LevelInfoSO startingLevel;

	private void Start() {
		LevelsManager.Instance.LoadLevel(startingLevel.level);
		LevelsManager.Instance.LoadLevels(startingLevel);
	}
}
