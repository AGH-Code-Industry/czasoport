using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataPersistence;
using LevelTimeChange.LevelsLoader;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {
    [SerializeField] private GameObject prefab;
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 targetPosition;

    public void Spawn() {
        Transform item = Instantiate(prefab).transform;
        item.parent = LevelsManager.Instance.LoadedLevels.First(a => a.Key.name == targetScene)
            .Value.levelContent.transform;
        item.position = targetPosition;
    }
}