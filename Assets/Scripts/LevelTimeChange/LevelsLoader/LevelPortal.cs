using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelTimeChange.LevelsLoader {
	public class LevelPortal : MonoBehaviour
	{
		[SerializeField] private LevelInfoSO destinedLevel;

		private void OnTriggerEnter2D(Collider2D other) {
			LevelsManager.Instance.ChangeLevel(destinedLevel);
		}
	}
}
