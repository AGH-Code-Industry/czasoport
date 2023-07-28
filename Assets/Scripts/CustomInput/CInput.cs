using UnityEngine;
using UnityEngine.Events;

namespace CustomInput {
    public static class CInput {
        public static readonly InputActions InputActions;
        
        public static Vector2 NavigationAxis => InputActions.Movement.Navigation.ReadValue<Vector2>();
        public static Vector2 MousePosition => InputActions.Mouse.MousePosition.ReadValue<Vector2>();
        public static float Run => InputActions.Movement.Run.ReadValue<float>();

        static CInput() {
            InputActions = new InputActions();
            InputActions.Enable();
        }
    }
}

