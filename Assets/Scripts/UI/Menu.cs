using DataPersistence;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    /// <summary>
    /// Script for main menu in game
    /// </summary>
    public class Menu : MonoBehaviour {
        [SerializeField] private Button playButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button quitButton;

        void Awake() {
            playButton.onClick.AddListener(PlayAction);
            continueButton.onClick.AddListener(ContinueAction);
            quitButton.onClick.AddListener(() => UnityEngine.Application.Quit());
        }

        /// <summary>
        /// Action called when user press Play button
        /// </summary>
        void PlayAction() {
            DataPersistenceManager.Instance.CreateNewGame();
            SceneManager.LoadScene(DeveloperSettings.Instance.appSettings.gameSceneName, LoadSceneMode.Single);
        }

        void ContinueAction() {
            DataPersistenceManager.Instance.LoadGameFromDisk();
            SceneManager.LoadScene(DeveloperSettings.Instance.appSettings.gameSceneName, LoadSceneMode.Single);
        }
    }
}