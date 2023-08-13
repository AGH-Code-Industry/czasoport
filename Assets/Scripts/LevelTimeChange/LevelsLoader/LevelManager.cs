using System;
using System.Collections.Generic;
using Application;
using CoinPackage.Debugging;
using UnityEngine;

namespace LevelTimeChange.LevelsLoader {
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

            LevelsManager.Instance.LoadedLevelsDict.Add(currentLevel, this);
            _logger.Log($"New scene has awoken: {currentLevel.sceneName % Colorize.Cyan}");
            
            FindTeleportsOnScene();
            DeactivateLevel();
        }

        public void ActivateLevel() {
            _logger.Log($"Scene {currentLevel.sceneName} is {"activating" % Colorize.Green}");
            levelContent.SetActive(true);
        }

        public void DeactivateLevel() {
            _logger.Log($"Scene {currentLevel.sceneName} is {"deactivating" % Colorize.Red}");
            levelContent.SetActive(false);
            LevelsManager.Instance.ReportForDiscovery(currentLevel);
        }

        public void MakeDiscovery(LevelInfoSO levelToDiscover) {
            _logger.Log($"Scene {currentLevel.sceneName % Colorize.Cyan} is making discovery with {levelToDiscover.sceneName % Colorize.Cyan}");
            foreach (var levelPortal in _teleports) {
                if (levelPortal.destinedLevel == levelToDiscover) {
                    levelPortal.MakeDiscovery(currentLevel);
                }
            }
        }

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
    }
}