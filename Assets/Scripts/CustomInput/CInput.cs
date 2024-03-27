using CustomInput.Exceptions;
using CustomInput.Locks;
using UnityEngine;

namespace CustomInput {
    /// <summary>
    /// Wrapper for automatically generated InputActions. Provides easier access to reading
    /// values from input, takes care of processing for common input values.
    /// Incorporates locking mechanism.
    /// </summary>
    public static class CInput {
        /// <summary>
        /// InputActions object that CInput is based on. Should be used if there is no wrapper
        /// implemented by CInput.
        /// </summary>
        public static readonly InputActions InputActions;

        /// <summary>
        /// Vector2 desired player direction/
        /// </summary>
        public static Vector2 NavigationAxis => InputActions.Movement.Navigation.ReadValue<Vector2>();

        /// <summary>
        /// Normal mouse position from InputActions. If you want point in game over which mouse is hovering,
        /// use `MouseWorldPosition`.
        /// </summary>
        public static Vector2 MousePosition => InputActions.Mouse.MousePosition.ReadValue<Vector2>();

        /// <summary>
        /// Mouse position casted to world coordinates.
        /// </summary>
        public static Vector2 MouseWorldPosition => GetMouseWorldPosition();

        /// <summary>
        /// Whether player is running, and how fast he is running (effective on gamepads).
        /// </summary>
        public static float Run => InputActions.Movement.Run.ReadValue<float>();


        public static readonly MovementLock MovementLock;
        public static readonly MouseLock MouseLock;
        public static readonly InventoryLock InventoryLock;
        public static readonly TeleportLock TeleportLock;
        public static readonly InteractionsLock InteractionsLock;
        public static readonly GameInputLock GameInputLock;

        static CInput() {
            InputActions = new InputActions();
            InputActions.Enable();

            MovementLock = new MovementLock(InputActions.Movement);
            MouseLock = new MouseLock(InputActions.Mouse);
            InventoryLock = new InventoryLock(InputActions.Inventory);
            TeleportLock = new TeleportLock(InputActions.Teleport);
            InteractionsLock = new InteractionsLock(InputActions.Interactions);
            GameInputLock = new GameInputLock(InputActions.Game);
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