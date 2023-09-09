using System;
using System.Collections.Generic;
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
        [SerializeField] private LevelInfoSO currentLevel;
        [Tooltip("Content object of this level.")]
        [SerializeField] private GameObject levelContent;
        [Tooltip("References to GameObjects that holds teleports.")]
        [SerializeField] private GameObject[] teleportsHolders;

        private List<LevelPortal> _teleports;
        private CLogger _logger = Loggers.LoggersList["LEVEL_SYSTEM"];

        private void Awake() {
            _teleports = new List<LevelPortal>();

            LevelsManager.Instance.LoadedLevels.Add(currentLevel, this);
            _logger.Log($"New scene has awoken: {currentLevel.sceneName % Colorize.Cyan}");
            
            FindTeleportsOnScene();
            PrepareLevelContent();
            DeactivateLevel();
        }

        /// <summary>
        /// It will set all level content to be active.
        /// Use it only when this level is going to be one played on by the player.
        /// </summary>
        public void ActivateLevel() {
            _logger.Log($"Scene {currentLevel.sceneName} is {"activating" % Colorize.Green}");
            levelContent.SetActive(true);
        }

        /// <summary>
        /// It will set all level content to be inactive and report to `LevelsManager` that
        /// this scene is ready for Discovery process. It is automatically called when
        /// scene is loaded.
        /// </summary>
        public void DeactivateLevel() {
            _logger.Log($"Scene {currentLevel.sceneName} is {"deactivating" % Colorize.Red}");
            levelContent.SetActive(false);
            LevelsManager.Instance.ReportForDiscovery(currentLevel);
        }

        /// <summary>
        /// Start discovery process with specified level. It tells portals on this scene
        /// to try to match with portals on another one.
        /// </summary>
        /// <param name="levelToDiscover">Level ready for discovery process.</param>
        public void MakeDiscovery(LevelInfoSO levelToDiscover) {
            _logger.Log($"Scene {currentLevel.sceneName % Colorize.Cyan} is making discovery with {levelToDiscover.sceneName % Colorize.Cyan}");
            foreach (var levelPortal in _teleports) {
                if (levelPortal.destinedLevel == levelToDiscover) {
                    levelPortal.MakeDiscovery(currentLevel);
                }
            }
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
            throw new Exception($"No match found for portal {sourceLevel} on {this}");
        }

        private void FindTeleportsOnScene() {
            foreach (var teleportsHolder in teleportsHolders) {
                var children = teleportsHolder.GetComponentsInChildren<LevelPortal>();
                foreach (var levelPortal in children) {
                    _teleports.Add(levelPortal);
                }
            }
        }

		private void PrepareLevelContent() {
			var timelines = FindTimelineMaps();
			if (timelines == null) {
				_logger.LogError($"Failed to find timelines in level {currentLevel.sceneName}." +
                                  "Please make sure there are Past, Present, Future game objects under Content game object.");
			}
			MoveTimelines(timelines, DeveloperSettings.Instance.tpcSettings.offsetFromPresentPlatform);
		}

		private TimelineMaps FindTimelineMaps() {
			var past = levelContent.transform.Find("Past")?.gameObject;
			var present = levelContent.transform.Find("Present")?.gameObject;
			var future = levelContent.transform.Find("Future")?.gameObject;
			if (past == null || present == null || future == null) {
				return null;
			}
			return new TimelineMaps(past, present, future);
		}

		private void MoveTimelines(TimelineMaps timelines, Vector3 offset) {
			timelines.past.transform.position = timelines.present.transform.position - offset;
			timelines.future.transform.position = timelines.present.transform.position + offset;
		}
    }

	class TimelineMaps {
		public GameObject past {get; set;}
		public GameObject present {get; set;}
		public GameObject future {get; set;}

		public TimelineMaps(GameObject past, GameObject present, GameObject future) {
			this.past = past;
			this.present = present;
			this.future = future;
		}
	}
}