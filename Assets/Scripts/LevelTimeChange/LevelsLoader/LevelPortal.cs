using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelTimeChange.LevelsLoader {
	/// <summary>
	/// This component is used to define teleport to another platform. Based on information provided to it,
	/// it tries to discover matching teleport on another scene. When successful, it is able to teleport
	/// player to this scene.
	/// </summary>
	public class LevelPortal : MonoBehaviour {
		[Tooltip("On which Timeline this teleport is.")]
		[SerializeField] public TimeLine teleportTimeline;
		[Tooltip("To what level this teleport teleports.")]
		[SerializeField] public LevelInfoSO destinedLevel;

		/// <summary>
		/// GameObject to which this teleports teleport.
		/// </summary>
		private Transform _teleportPoint;

		/// <summary>
		/// Portal that matches this portal on another scene.
		/// </summary>
		private LevelPortal _matchingPortal;

		/// <summary>
		/// Returns point in the world this teleports point to.
		/// </summary>
		/// <returns>Point in game.</returns>
		public Vector3 GetTeleportPoint() {
			return _teleportPoint.transform.position;
		}

		public void MakeDiscovery(LevelInfoSO currentLevel) {
			var destinedLevelManager = LevelsManager.Instance.LoadedLevelsDict[currentLevel];
			_matchingPortal = destinedLevelManager.ReturnMatchingPortal(currentLevel, this);
		}

		public bool IsMatch(LevelInfoSO sourceLevel, LevelPortal sourcePortal) {
			if (sourceLevel == destinedLevel && sourcePortal.teleportTimeline == this.teleportTimeline) {
				return true;
			}
			return false;
		}
		
		private void Awake() {
			_teleportPoint = transform.GetChild(0);
		}

		private void OnTriggerEnter2D(Collider2D other) {
			LevelsManager.Instance.ChangeLevel(destinedLevel, this);
		}
	}
}
