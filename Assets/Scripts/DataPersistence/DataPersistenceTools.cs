using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataPersistence;
using InteractableObjectSystem;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DataPersistence {
    public class DataPersistenceTools : MonoBehaviour {
        [MenuItem("Tools/Data/RandomizeIDs")]
        private static void RandomizeIDs() {
            if (!UnityEngine.Application.isEditor) {
                Debug.LogError("This function can only be called in the editor!");
                return;
            }

            var objects = new List<MonoBehaviour>();

            objects.AddRange(FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(obj => obj is IPersistentObject));

            foreach (var obj in objects) {
                var persistentObject = obj as IPersistentObject;
                if (persistentObject == null)
                    continue;

                persistentObject.ID = Guid.NewGuid();
                EditorUtility.SetDirty(obj);
            }

            EditorSceneManager.MarkAllScenesDirty();
            EditorSceneManager.SaveOpenScenes();
        }
    }
}

#endif