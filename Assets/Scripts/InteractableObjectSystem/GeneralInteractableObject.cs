using Items;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableObjectSystem.Objects {

    public class GeneralInteractableObject : InteractableObject
    {
        public UnityEvent onEnabled;
        public UnityEvent onDisabled;

        private State state = State.Off;

        private enum State {
            On,
            Off
        }

        public override void InteractionHand() {
            toggleState();
        }

        public override void LongInteractionHand() {
            toggleState();
        }

        public override bool InteractionItem(Item item) {
            toggleState();
            return false;
        }

        public override bool LongInteractionItem(Item item) {
            toggleState();
            return false;
        }

        private void toggleState() {
            if (state == State.On) {
                state = State.Off;
                onDisabled.Invoke();
            } else {
                state = State.On;
                onEnabled.Invoke();
            }
        }

    }

}
