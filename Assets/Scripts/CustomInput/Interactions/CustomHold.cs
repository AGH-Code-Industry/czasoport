using UnityEngine.InputSystem;

namespace CustomInput.Interactions
{
	public class CustomHold : IInputInteraction
	{
		public float duration = 0.4f;
		public float pressPoint = 0.5f;

		private double timePressed;
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.PerformedAndStayPerformed();
				return;
			}

			switch (context.phase)
			{
				case InputActionPhase.Waiting:
					if (context.ControlIsActuated(pressPoint))
					{
						timePressed = context.time;

						context.Started();
						context.SetTimeout(duration);
					}
					break;

				case InputActionPhase.Started:
					if (context.time - timePressed >= duration)
					{
						context.PerformedAndStayPerformed();
					}
					if (!context.ControlIsActuated())
					{
						context.Waiting();
					}
					break;

				case InputActionPhase.Performed:
					if (!context.ControlIsActuated(pressPoint))
						context.Canceled();
					break;
			}
		}

		public void Reset()
		{
			timePressed = 0;
		}

	}
}
