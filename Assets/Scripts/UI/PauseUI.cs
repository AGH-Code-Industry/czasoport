using UnityEngine;
using UnityEngine.UI;
using CustomInput;
using UnityEngine.InputSystem;
using CoinPackage.Debugging;
using Application;
using DataPersistence;

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
            saveButton.onClick.AddListener(Save);
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
            resumeButton.transform.localScale = Vector3.zero;
            saveButton.transform.localScale = Vector3.zero;
            quitButton.transform.localScale = Vector3.zero;
            resumeButton.transform.parent.transform.localScale = Vector3.zero;
            LeanTween.scale(resumeButton.gameObject, new Vector3(1f, 1f, 0f), 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBounce);
            LeanTween.scale(saveButton.gameObject, new Vector3(1f, 1f, 0f), 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBounce).setDelay(0.1f);
            LeanTween.scale(quitButton.gameObject, new Vector3(1f, 1f, 0f), 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBounce).setDelay(0.2f);
            LeanTween.scale(resumeButton.transform.parent.gameObject, new Vector3(1f, 1f, 0f), 0.3f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutQuint);
            Time.timeScale = 0f;
        }

        private void Save() {
            DataPersistenceManager.Instance.SaveGame();
        }
        
        private void Show() {
            menuPanel.SetActive(true);
        }

        private void Hide() {
            menuPanel.SetActive(false);
        }

        public bool IsGamePaused() { return _gamePaused; }
    }
}