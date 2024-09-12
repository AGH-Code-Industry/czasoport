using System;
using CoinPackage.Debugging;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.Singleton;

namespace Dialogues {
    public class DialogueManager : Singleton<DialogueManager> {
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private GameObject choicesPanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private GameObject choicePrefab;

        [SerializeField] private Button exitButton;
        [SerializeField] private Button continueButton;

        [NonSerialized] public UnityEvent dialogueStarted;
        [NonSerialized] public UnityEvent dialogueEnded;

        private readonly CLogger _logger = Application.Loggers.LoggersList[Application.Loggers.LoggerType.DIALOGUES];


        private ChoicesProcessor _choicesProcessor;
        private Story _currentStory;
        private bool _dialogueActive;
        private bool _hasAvailableChoices;
        private Action _functionToCallback;

        protected override void Awake() {
            base.Awake();
            dialogueStarted = new UnityEvent();
            dialogueEnded = new UnityEvent();

            _choicesProcessor = new ChoicesProcessor(choicesPanel, choicePrefab);

            exitButton.onClick.AddListener(() => EndDialogue());
            continueButton.onClick.AddListener(() => ContinueDialogue());
        }

        private void Start() {
            dialoguePanel.SetActive(false);
            dialogueText.SetText("No dialogues playing. If you see this, you have a bug.");
        }

        /// <summary>
        /// Start new dialogue from a file. File must be provided by other Behaviour.
        /// Dialogue will only start if there is no other dialogue active.
        /// </summary>
        /// <param name="storyFile">File from which to load the story. Must be a JSON file, generated from Ink file.</param>
        /// <param name="finishAction">This action will be called when dialogues ends</param>
        public bool StartDialogue(TextAsset storyFile, Action finishAction = null) {
            if (_dialogueActive) {
                _logger.LogWarning("DialogueManager is already playing another dialogue.");
                return false;
            }

            _functionToCallback = finishAction;
            _currentStory = new Story(storyFile.text);
            _dialogueActive = true;
            dialoguePanel.SetActive(true);
            dialogueStarted.Invoke();
            ContinueDialogue();
            return true;
        }

        /// <summary>
        /// End the current dialogue. If there is no dialogue active, nothing will happen. Dialogue progress won't be saved.
        /// </summary>
        public void EndDialogue() {
            if (!_dialogueActive) {
                return;
            }
            _currentStory = null;
            _dialogueActive = false;
            dialoguePanel.SetActive(false);
            dialogueEnded.Invoke();
            _functionToCallback?.Invoke();
        }

        /// <summary>
        /// Main function in the dialogue process, manages choices parsing, tag parsing and canvas updating.
        /// Will end the dialogue if there are no more lines to read.
        /// </summary>
        public void ContinueDialogue() {
            if (!_currentStory.canContinue) {
                EndDialogue();
                return;
            }
            
            _currentStory.Continue();
            if (_currentStory.state.currentChoices.Count == 0 && _currentStory.state.currentText == "") {
                EndDialogue();
                return;
            }
            _hasAvailableChoices = _choicesProcessor.ProcessChoices(_currentStory);
            UpdateDialogueBox();
        }

        private void UpdateDialogueBox() {
            dialogueText.SetText(_currentStory.currentText);
            continueButton.gameObject.SetActive(!_hasAvailableChoices);
        }
    }
}