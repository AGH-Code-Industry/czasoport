using UnityEngine.InputSystem;

namespace CustomInput.Interactions {
    public class CustomHold : IInputInteraction {
        public float MinimalTime = 0.2f;

        public void Process(ref InputInteractionContext context) {
            if (context.timerHasExpired) {
                context.PerformedAndStayPerformed();
                return;
            }

            switch (context.phase) {
                case InputActionPhase.Waiting:
                    if (context.ControlIsActuated(MinimalTime)) {
                        context.Started();
                        context.SetTimeout(MinimalTime);
                    }
                    break;

                case InputActionPhase.Started:
                    if (!context.ControlIsActuated()) {
                        context.Waiting();
                    }
                    break;

                case InputActionPhase.Performed:
                    if (!context.ControlIsActuated(MinimalTime))
                        context.Canceled();
                    break;
            }
        }

        public void Reset() { }

    }
}