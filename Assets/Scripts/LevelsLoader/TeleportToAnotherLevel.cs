using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelsLoader {
	public class TeleportToAnotherLevel : MonoBehaviour
	{
		[SerializeField] private LevelInfoSO destinedLevel;

		private void OnTriggerEnter2D(Collider2D collision) {
			LevelsManager.Instance.ChangeLevel(destinedLevel);
		}
	}
}
