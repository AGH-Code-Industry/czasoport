using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using LevelTimeChange.LevelsLoader;
using UnityEngine;

namespace InteractableObjectSystem {
    public class SpawnInAnyScene : MonoBehaviour {
        [SerializeField] private ItemSO itemSO;
        [SerializeField] private string targetScene;
        [SerializeField] private Vector3 targetPosition;

        public void Spawn() {
            Transform item = Instantiate(itemSO.prefab).transform;
            item.parent = LevelsManager.Instance.LoadedLevels.First(a => a.Key.name == targetScene)
                .Value.levelContent.transform;
            item.position = targetPosition;
        }
    }
}