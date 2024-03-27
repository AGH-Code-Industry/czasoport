using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using LevelTimeChange.LevelsLoader;
using UnityEngine;

namespace Application {
    public class GameManager : MonoBehaviour {
        void Start() {
            // Update objects with game data loaded in the menu
            DataPersistenceManager.Instance.LoadGame();

            // Load first scene
            LevelsManager.Instance.LoadFirstLevel();
        }
    }
}