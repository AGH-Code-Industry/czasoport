using CoinPackage.Debugging;
using CustomInput;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class Torch : Object {

        [SerializeField] private Sprite TorchLighting;
        [SerializeField] private Sprite TorchNotLighting;

        enum State {
            Lighting,
            NotLighting
        }

        private State state;

        private SpriteRenderer spriteRenderer;

        //for test
        private bool Interactable;

        private void OnEnable() {
            CInput.InputActions.Teleport.TeleportForward.performed += ToogleThisItem;
        }

        private void ToogleThisItem(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            Interactable = !Interactable;
            if (Interactable) {
                CDebug.Log("You interacting with torch");
            }
            else {
                CDebug.Log("You dont interacting with torch");
            }
        }

        private void Awake() {
            state = State.Lighting;
            spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateVisual();
        }

        public override void InteractionItem(Item item) {
            if (item.GetItemSO().id == 9) {
                ChangeStateToNotLighing();
            }
        }

        private void ChangeStateToLighting() {
            state = State.Lighting;
            UpdateVisual();
        }

        private void ChangeStateToNotLighing() {
            state = State.NotLighting;
            UpdateVisual();
        }

        private void UpdateVisual() {
            switch (state) {
                case State.Lighting:
                    spriteRenderer.sprite = TorchLighting;
                    break;
                case State.NotLighting:
                    spriteRenderer.sprite = TorchNotLighting;
                    break;
            }
        }

    }
}
