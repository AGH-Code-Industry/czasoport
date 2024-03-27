using System;
using System.Collections;
using System.Collections.Generic;
using Application;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelTimeChange.LevelsLoader {
    /// <summary>
    /// This component is used to define teleport to another platform. Based on information provided to it,
    /// it tries to discover matching teleport on another scene. When successful, it is able to teleport
    /// player to this scene.
    /// </summary>
    public class LevelPortal : MonoBehaviour {
        [Tooltip("To what level this teleport teleports.")]
        [SerializeField] public LevelInfoSO destinedLevel;
        [FormerlySerializedAs("_teleportPoint")]
        [Tooltip("Object that marks where player should be teleported to.")]
        [SerializeField] private Transform teleportPoint;

        /// <summary>
		/// Portal that matches this portal on another scene.
		/// </summary>
		private LevelPortal _matchingPortal;


        /// <summary>
        /// TimeLine the portal is on
        /// </summary>
        private TimeLine _teleportTimeline;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.PORTALS];

        /// <summary>
        /// Set the timeline of the portal. Used during loading scenes.
        /// </summary>
        /// <param name="timeLine"></param>
        public void SetTimeLine(TimeLine timeLine) {
            _teleportTimeline = timeLine;
        }

        /// <summary>
		/// Returns point in the world this teleports point to.
		/// </summary>
		/// <returns>Point in game.</returns>
		public Vector3 GetTeleportPoint() {
            return teleportPoint.transform.position;
        }

        /// <summary>
        /// Start discovery process with destined level. Invoking this function on it's own will not ensure
        /// that destined level is loaded and can take part in discovery process.
        /// </summary>
        /// <param name="currentLevel">Source level of discovery.</param>
        public void MakeDiscovery(LevelInfoSO currentLevel) {
            _matchingPortal = LevelsManager.Instance.LoadedLevels[destinedLevel]
                .ReturnMatchingPortal(currentLevel, this);
            _logger.Log($"Discovery match for portal {this}: {_matchingPortal}");
        }

        /// <summary>
        /// Check if provided portal matches this portal.
        /// </summary>
        /// <param name="sourceLevel">Source level.</param>
        /// <param name="sourcePortal">Potential level match.</param>
        /// <returns>`True` if portal is a match, `False` otherwise.</returns>
        public bool IsMatch(LevelInfoSO sourceLevel, LevelPortal sourcePortal) {
            if (sourceLevel == destinedLevel && sourcePortal._teleportTimeline == this._teleportTimeline) {
                return true;
            }
            return false;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            _logger.Log($"Portal {this} activated, teleporting to {_matchingPortal}");
            LevelsManager.Instance.ChangeLevel(destinedLevel, _matchingPortal);
        }

        public override string ToString() {
            return $"[Portal, TL: {_teleportTimeline}, DST: {destinedLevel}]" % Colorize.Cyan;
        }
    }
}