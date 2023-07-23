using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application {
    public class StartupManager : MonoBehaviour
    {
        private void Start() {
            LoadGlobalObjects();

            // Load scene with Single mode, all objects that should not be unloaded
            // must have non-destroyable class.
            SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        }

        private void LoadGlobalObjects() {
            StartCoroutine(LoadSceneAsynchronously("Persistent", LoadSceneMode.Additive));
        }

        private IEnumerator LoadSceneAsynchronously(string sceneName, LoadSceneMode mode) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);
            while (!asyncLoad.isDone) {
                yield return null;
            }
        }
    }
}

