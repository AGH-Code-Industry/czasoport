using UnityEngine;
using UnityEngine.Events;

namespace InputSystem {
    public static class CInput {
        public static readonly InputActions InputActions;
        
        public static Vector2 MovementAxis => InputActions.Game.Movement.ReadValue<Vector2>();
        public static Vector2 MousePosition => InputActions.Mouse.MousePosition.ReadValue<Vector2>();
        public static float Run => InputActions.Game.Run.ReadValue<float>();

        static CInput() {
            InputActions = new InputActions();
            InputActions.Enable();
        }
    }
}

