using System;
using CustomInput;
using Interactions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class CodeReader : InteractableObject {

        [SerializeField] private GameObject codeUI;
        [SerializeField] private ScreenUI screenUI;

        private int _code;
        private void Awake() {
            codeUI.SetActive(false);
            _code = Random.Range(1000, 10000);
            screenUI.SetCode(_code);
        }

        public override void InteractionHand() {
            codeUI.SetActive(true);
            CInput.InputActions.Movement.Disable();
        }
    }
}