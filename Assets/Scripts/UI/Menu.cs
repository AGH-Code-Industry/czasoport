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
		[SerializeField] private Button quitButton;

		void Awake() {
			playButton.onClick.AddListener(PlayAction);
			quitButton.onClick.AddListener(() => UnityEngine.Application.Quit());
		}

		/// <summary>
		/// Action called when user press Play button
		/// </summary>
		void PlayAction() {
			SceneManager.LoadScene(DeveloperSettings.Instance.appSettings.sceneToLoadName, LoadSceneMode.Additive);
			SceneManager.UnloadSceneAsync(gameObject.scene);
		}
	}
}
