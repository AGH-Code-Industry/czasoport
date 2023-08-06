using UnityEngine.InputSystem;

namespace CustomInput.Interactions
{
	public class CustomHold : IInputInteraction
	{	
		public float minimalTime = 0.2f;

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
					if (context.ControlIsActuated(minimalTime))
					{
						timePressed = context.time;

						context.Started();
						context.SetTimeout(minimalTime);
					}
					break;

				case InputActionPhase.Started:
					if (!context.ControlIsActuated())
					{
						context.Waiting();
					}
					break;

				case InputActionPhase.Performed:
					if (!context.ControlIsActuated(minimalTime))
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
