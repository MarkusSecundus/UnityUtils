#if USE_NEW_INPUT


using MarkusSecundus.Utils.Serialization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;



namespace MarkusSecundus.Utils.Behaviors.Actions
{
    /// <summary>
    /// Simple action that listens for keypresses and fires events registered for particular keys
    /// </summary>
    public class InputActionPerformedEvent : MonoBehaviour
    {

        /// <summary>
        /// Map of events to be invoked for specific keys being pressed
        /// </summary>
        public SerializableDictionary<string, UnityEvent> Events;

		private void Start()
        {
			(string Name, InputAction Action, UnityEvent ToPerform)[] actions = Events.Values.Select(kv => (kv.Key, InputSystem.actions.FindAction(kv.Key), kv.Value)).ToArray();

			InputSystem.onEvent += (InputEventPtr a, InputDevice b) =>
			{
				foreach (var (key, action, @event) in actions)
				{
					if (action.WasPerformedThisFrame())
					{
						@event.Invoke();
					}
				}
			};

		}
	}
}
#endif