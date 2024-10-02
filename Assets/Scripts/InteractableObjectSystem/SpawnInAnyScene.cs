using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataPersistence;
using Items;
using LevelTimeChange.LevelsLoader;
using UnityEngine;
using Utils;

namespace InteractableObjectSystem {
    public class SpawnInAnyScene : MonoBehaviour {
        [SerializeField] private SerializableGuid id;
        [SerializeField] private ItemSO itemSO;
        [SerializeField] private string targetScene;
        [SerializeField] private Vector3 targetPosition;

        public void Spawn() {
            Transform item = Instantiate(itemSO.prefab).transform;
            Item i = item.GetComponent<Item>();
            i.ID = id;
            item.parent = LevelsManager.Instance.LoadedLevels.First(a => a.Key.name == targetScene)
                .Value.levelContent.transform;
            item.position = targetPosition;
            i.LoadPersistentData(DataPersistenceManager.Instance.gameData);
        }
    }
}