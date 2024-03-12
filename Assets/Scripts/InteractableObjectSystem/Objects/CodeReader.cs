using System;
using CustomInput;
using Interactions;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class CodeReader : InteractableObject {

        [SerializeField] private GameObject codeUI;

        private void Awake() {
            codeUI.SetActive(false);
        }

        public override void InteractionHand() {
            codeUI.SetActive(true);
            CInput.InputActions.Movement.Disable();
        }
    }
}