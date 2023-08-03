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

        public static readonly InputLock<InputActions.MovementActions> MovementLock;
        public static readonly InputLock<InputActions.MouseActions> MouseLock;
        public static readonly InputLock<InputActions.InventoryActions> InventoryLock;
        public static readonly InputLock<InputActions.TeleportActions> TeleportLock;
        public static readonly InputLock<InputActions.InteractionsActions> InteractionsLock;

        static CInput() {
            InputActions = new InputActions();
            InputActions.Enable();

            MovementLock = new InputLock<InputActions.MovementActions>(InputActions.Movement);
            MouseLock = new InputLock<InputActions.MouseActions>(InputActions.Mouse);
            InventoryLock = new InputLock<InputActions.InventoryActions>(InputActions.Inventory);
            TeleportLock = new InputLock<InputActions.TeleportActions>(InputActions.Teleport);
            InteractionsLock = new InputLock<InputActions.InteractionsActions>(InputActions.Interactions);
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

