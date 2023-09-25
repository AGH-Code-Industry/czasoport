using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using CoinPackage.Debugging;
using UnityEngine;
using Settings;

namespace LevelTimeChange.LevelsLoader {
    /// <summary>
    /// `LevelManager` is responsible for managing one 'level platform'. It activates and deactivates
    /// level content, talks with `LevelsManager` and `LevelPortal`s to perform `Discovery` process.
    /// </summary>
    public class LevelManager : MonoBehaviour {
        [Tooltip("Asset for current level.")]
        public LevelInfoSO currentLevel;
        [Tooltip("Content object of this level.")]
        [SerializeField] private GameObject levelContent;

        [Header("References to GameObjects that holds teleports.")]
        [SerializeField] private GameObject pastPortalHolder;
        [SerializeField] private GameObject presentPortalHolder;
        [SerializeField] private GameObject futurePortalHolder;

        private List<LevelPortal> _teleports;
        private CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.LEVEL_SYSTEM];

        public List<LevelInfoSO> neighborLevels = new List<LevelInfoSO>();

        private void Awake() {
            _teleports = new List<LevelPortal>();

            _logger.Log($"New scene has awoken: {currentLevel}");
            LevelsManager.Instance.LoadedLevels.Add(currentLevel, this);
            _logger.Log($"New scene has awoken: {currentLevel.sceneName % Colorize.Cyan}");
            FindTeleportsOnScene();
            SetTimelinesPositions();
            DeactivateLevel();
            FindNeighbouringLevels();
        }

        /// <summary>
        /// It will set all level content to be active.
        /// Use it only when this level is going to be one played on by the player.
        /// </summary>
        public void ActivateLevel() {
            _logger.Log($"Scene {currentLevel} is {"activating" % Colorize.Green}");
            levelContent.SetActive(true);
        }

        /// <summary>
        /// It will set all level content to be inactive and report to `LevelsManager` that
        /// this scene is ready for Discovery process. It is automatically called when
        /// scene is loaded.
        /// </summary>
        public void DeactivateLevel() {
            _logger.Log($"Scene {currentLevel} is {"deactivating" % Colorize.Red}");
            levelContent.SetActive(false);
            LevelsManager.Instance.ReportForDiscovery(currentLevel);
        }

        /// <summary>
        /// Start discovery process with specified level. It tells portals on this scene
        /// to try to match with portals on another one.
        /// </summary>
        /// <param name="levelToDiscover">Level ready for discovery process.</param>
        public void MakeDiscovery(LevelInfoSO levelToDiscover) {
            _logger.Log($"Scene {currentLevel} {"started" % Colorize.Green} discovery with {levelToDiscover}");
            foreach (var levelPortal in _teleports) {
                if (levelPortal.destinedLevel == levelToDiscover) {
                    levelPortal.MakeDiscovery(currentLevel);
                }
            }
            _logger.Log($"Scene {currentLevel} {"finished" % Colorize.Orange} discovery with {levelToDiscover}");
            // TODO: Remove
            // foreach (var teleport in _teleports) {
            //     _logger.Log($"Match for portal {teleport} is {teleport._matchingPortal}");
            // }
        }

        /// <summary>
        /// Used in discovery process. For specified portal and source level, return matching portal on this scene.
        /// </summary>
        /// <param name="sourceLevel">Level that asks for discovery.</param>
        /// <param name="sourcePortal">Specific portal that asks for discovery.</param>
        /// <returns>Matching portal.</returns>
        /// <exception cref="Exception">Exception is thrown when there is no matching portal.</exception>
        public LevelPortal ReturnMatchingPortal(LevelInfoSO sourceLevel, LevelPortal sourcePortal) {
            foreach (var portal in _teleports) {
                if (portal.IsMatch(sourceLevel, sourcePortal)) {
                    return portal;
                }
            }
            throw new Exception($"No match found for portal {sourcePortal} on {currentLevel}");
        }

        /// <summary>
        /// Sets object's parent to current scene's content.
        /// </summary>
        /// <param name="levelObject">Object that should be part of the current scene</param>
        public void AddLevelObject(GameObject levelObject) {
            levelObject.transform.SetParent(levelContent.transform);
        }

        private void FindTeleportsOnScene() {
            if (pastPortalHolder) {
                var children = pastPortalHolder.GetComponentsInChildren<LevelPortal>();
                foreach (var levelPortal in children) {
                    levelPortal.SetTimeLine(TimeLine.Past);
                    _teleports.Add(levelPortal);
                }
            }
            if (presentPortalHolder) {
                var children = presentPortalHolder.GetComponentsInChildren<LevelPortal>();
                foreach (var levelPortal in children) {
                    levelPortal.SetTimeLine(TimeLine.Present);
                    _teleports.Add(levelPortal);
                }
            }
            if (futurePortalHolder) {
                var children = futurePortalHolder.GetComponentsInChildren<LevelPortal>();
                foreach (var levelPortal in children) {
                    levelPortal.SetTimeLine(TimeLine.Future);
                    _teleports.Add(levelPortal);
                }
            }
        }

		private void SetTimelinesPositions() {
			var timelines = FindTimelineMaps();
			if (timelines == null) {
				_logger.LogError($"Failed to find timelines in level {currentLevel.sceneName}." +
                                  "Please make sure there are Past, Present, Future game objects under Content game object.");
			}
			MoveTimelines(timelines, DeveloperSettings.Instance.tpcSettings.offsetFromPresentPlatform);
		}

		private TimelineMaps FindTimelineMaps() {
			var past = levelContent.transform.Find("Past");
			var present = levelContent.transform.Find("Present");
			var future = levelContent.transform.Find("Future");
			if (past == null || present == null || future == null) {
				return null;
			}
			return new TimelineMaps(past, present, future);
		}

		private void MoveTimelines(TimelineMaps timelines, Vector3 offset) {
			timelines.past.position = timelines.present.position - offset;
			timelines.future.position = timelines.present.position + offset;
		}


        /// <summary>
        /// Finds neighboring levels based on where portals are in the current level lead.
        /// </summary>
        private void FindNeighbouringLevels()
        {
            List<LevelInfoSO> neighourLevelsList = new List<LevelInfoSO>();

            foreach (LevelPortal teleport in _teleports) {
                if (!neighourLevelsList.Contains(teleport.destinedLevel)) {
                    neighourLevelsList.Add(teleport.destinedLevel);
                }
            }
            neighborLevels = neighourLevelsList.ToList();
        }
    }

	class TimelineMaps {
		public Transform past {get; set;}
		public Transform present {get; set;}
		public Transform future {get; set;}

		public TimelineMaps(Transform past, Transform present, Transform future) {
			this.past = past;
			this.present = present;
			this.future = future;
		}
	}
}