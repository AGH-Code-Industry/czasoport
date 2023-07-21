using UnityEngine;
using UnityEngine.Events;

namespace InputSystem {
    public static class CInput {
        public static readonly InputActions InputActions;
        
        public static Vector2 MovementAxis => InputActions.InGame.Movement.ReadValue<Vector2>();
        public static float Run => InputActions.InGame.Run.ReadValue<float>();

        static CInput() {
            InputActions = new InputActions();
            InputActions.Enable();
        }
    }
}

