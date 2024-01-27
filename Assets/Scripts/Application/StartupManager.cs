using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application {
    public class StartupManager : MonoBehaviour
    {
        private void Start() {
            // Unity loads all gameObjects associated with this class and after that we go to the menu
            // Objects that we want to be persistent during entire application must use DontDestroyOnLoad()
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}

