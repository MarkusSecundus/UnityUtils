using MarkusSecundus.Utils.Extensions;
using MarkusSecundus.Utils.Input;
using MarkusSecundus.Utils.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils.Behaviors.Actions
{
    /// <summary>
    /// Simple action that listens for keypresses and fires events registered for particular keys
    /// </summary>
    public class KeyPressEvent : MonoBehaviour
    {
        public KeyInputSource InputSource;
        /// <summary>
        /// Map of events to be invoked for specific keys being pressed
        /// </summary>
        public SerializableDictionary<KeyCode, UnityEvent> Events;

        void Update()
        {
            if (InputSource)
            {
                if (InputSource.IsAnyKeyDown)
                {
                    foreach (var (key, @event) in Events.Values)
                    {
                        if (InputSource.GetKeyDown(key))
                            @event.Invoke();
                    }
                }
            }
            else
            {
                if (UnityEngine.Input.anyKeyDown)
                {
                    foreach (var (key, @event) in Events.Values)
                    {
                        if (UnityEngine.Input.GetKeyDown(key))
                            @event.Invoke();
                    }
                }
            }
        }
    }
}