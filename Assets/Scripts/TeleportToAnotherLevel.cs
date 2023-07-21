using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToAnotherLevel : MonoBehaviour
{
	[SerializeField] private LevelInfoSO levelInfo;

	private void OnTriggerEnter2D(Collider2D collision) {
		LevelsManager.Instance.LoadLevels(levelInfo);
	}

}
