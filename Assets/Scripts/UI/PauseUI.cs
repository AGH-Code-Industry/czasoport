using UnityEngine;
using UnityEngine.UI;
using CustomInput;
using UnityEngine.InputSystem;
using CoinPackage.Debugging;
using Application;

namespace UI {
    /// <summary>
    /// Script for pause menu in game
    /// </summary>
    public class PauseUI : MonoBehaviour {
        
        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.PAUSE];

        [SerializeField] private GameObject menuPanel;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button quitButton;

        private bool _gamePaused;

        private void Awake() {
            CInput.InputActions.Game.TogglePause.performed += Toggle_PauseOnPerformed;
            
            resumeButton.onClick.AddListener(ResumeGame);
            quitButton.onClick.AddListener(UnityEngine.Application.Quit);
            
            Hide();
        }
        private void Toggle_PauseOnPerformed(InputAction.CallbackContext obj) {
            if (_gamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        private void ResumeGame() {
            _gamePaused = false;
            _logger.Log("Game resumed", Colorize.Green);
            Time.timeScale = 1f;
            Hide();
        }

        private void PauseGame() {
            _gamePaused = true;
            _logger.Log("Game paused", Colorize.Red);
            Show();
            Time.timeScale = 0f;
        }

        private void Show() {
            menuPanel.SetActive(true);
        }

        private void Hide() {
            menuPanel.SetActive(false);
        }
    }
}