using System;
using System.Collections.Generic;
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

        private void Awake() {
            _teleports = new List<LevelPortal>();
            
            FindTeleportsOnScene();
            DeactivateLevel();
        }

        public void ActivateLevel() {
            levelContent.SetActive(true);
        }

        public void DeactivateLevel() {
            levelContent.SetActive(false);
        }

        public void MakeDiscovery() {
            foreach (var levelPortal in _teleports) {
                levelPortal.MakeDiscovery(currentLevel);
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