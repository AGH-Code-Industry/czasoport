using InteractableObjectSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiningQuestInit : InteractableObject {
    /*
        QUEST INITIALIZATION:
        1. Q_Mining_Start - shovel that starts the quest. Place on the Rafinery scene
        2. Quest_Mining - place as a child object of Player in scene Game - script gets player position
        3. place NPC_Mining && NPC_Scientist on the Rafinery scene
     */
    [SerializeField] private GameObject _miningQuestGO;

    private void Start() {

        StartCoroutine(getMiningQuest());
    }
    GameObject FindGameObjectByTagInScene(string sceneName, string rootTag, string childName) {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        if (targetScene.isLoaded) {
            GameObject[] rootObjects = targetScene.GetRootGameObjects();
            
            foreach(GameObject obj in rootObjects) {
                if (obj.CompareTag(rootTag))
                    return obj.transform?.Find(childName).gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// It does indeed wait for 5 seconds, and I think it's only because of testing it in new tutorial scene when a lot of scenes (including Game) loads simultaneously
    /// Hardcoded but it finally works.
    /// </summary>
    /// <returns></returns>
    IEnumerator getMiningQuest() {
        yield return new WaitForSeconds(5);
        Debug.Log(SceneManager.GetSceneByName("Game").isLoaded);
        _miningQuestGO = FindGameObjectByTagInScene("Game", "Player", "Quest_Mining");
        Debug.Log(_miningQuestGO.name);
    }
    public override void InteractionHand() {

        if(tag == "Q_mining") _miningQuestGO.SetActive(!_miningQuestGO.activeSelf);
    }
}

