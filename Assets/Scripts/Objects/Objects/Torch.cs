using CoinPackage.Debugging;
using CustomInput;
using Items;
using UnityEngine;

namespace Objects.Objects {
    public class Torch : InteractableObject {

        [SerializeField] private Sprite torchLighting;
        [SerializeField] private Sprite torchNotLighting;

        enum State {
            Lighting,
            NotLighting
        }

        private State state;

        private SpriteRenderer spriteRenderer;

        private void Awake() {
            state = State.Lighting;
            spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateVisual();
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
                    spriteRenderer.sprite = torchLighting;
                    break;
                case State.NotLighting:
                    spriteRenderer.sprite = torchNotLighting;
                    break;
            }
        }

    }
}
