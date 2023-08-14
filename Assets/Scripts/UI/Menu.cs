using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        playButton.onClick.AddListener(PlayAction);
        quitButton.onClick.AddListener(() => UnityEngine.Application.Quit());
    }

    void PlayAction() {
		SceneManager.LoadSceneAsync(DeveloperSettings.Instance.appSettings.sceneToLoadName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
	}


}
