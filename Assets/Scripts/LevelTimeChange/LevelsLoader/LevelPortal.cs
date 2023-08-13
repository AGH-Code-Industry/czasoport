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

		/// <summary>
		/// Start discovery process with destined level. Invoking this function on it's own will not ensure
		/// that destined level is loaded and can take part in discovery process.
		/// </summary>
		/// <param name="currentLevel">Source level of discovery.</param>
		public void MakeDiscovery(LevelInfoSO currentLevel) {
			_matchingPortal = LevelsManager.Instance.LoadedLevels[destinedLevel]
				.ReturnMatchingPortal(currentLevel, this);
		}

		/// <summary>
		/// Check if provided portal matches this portal.
		/// </summary>
		/// <param name="sourceLevel">Source level.</param>
		/// <param name="sourcePortal">Potential level match.</param>
		/// <returns>`True` if portal is a match, `False` otherwise.</returns>
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
			LevelsManager.Instance.ChangeLevel(destinedLevel, _matchingPortal);
		}
	}
}
