using CustomInput.Exceptions;
using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.Events;

namespace CustomInput {
    public static class CInput {
        public static readonly InputActions InputActions;
        
        public static Vector2 NavigationAxis => InputActions.Movement.Navigation.ReadValue<Vector2>();
        public static Vector2 MousePosition => InputActions.Mouse.MousePosition.ReadValue<Vector2>();
        public static Vector2 MouseWorldPosition => GetMouseWorldPosition();
        public static float Run => InputActions.Movement.Run.ReadValue<float>();

        static CInput() {
            InputActions = new InputActions();
            InputActions.Enable();
        }

        private static Vector2 GetMouseWorldPosition() {
            if (Camera.main is not null) {
                return Camera.main.ScreenToWorldPoint(MousePosition);
            }
            throw new NoMainCameraException(
                "Tried to access CInput.MouseWorldPosition with no object with tag 'MainCamera' present in the loaded scenes.");
        }
    }
}

