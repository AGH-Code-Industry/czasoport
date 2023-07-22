using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToAnotherLevel : MonoBehaviour
{
	[SerializeField] private LevelInfoSO levelInfo;

	private void OnTriggerEnter2D(Collider2D collision) {
		ChangeLevel(levelInfo);
	}

	private void ChangeLevel(LevelInfoSO levelInfo) {
		LevelsManager.Instance.ChangeLevel(levelInfo);

	}

}
