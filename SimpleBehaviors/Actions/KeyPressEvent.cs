using MarkusSecundus.Utils.Extensions;
using MarkusSecundus.Utils.Input;
using MarkusSecundus.Utils.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace MarkusSecundus.Utils.Behaviors.Actions
{
    /// <summary>
    /// Simple action that listens for keypresses and fires events registered for particular keys
    /// </summary>
    public class KeyPressEvent : MonoBehaviour
    {
        public IKeyInputSource InputSource { get => _inputSource_field ?? _inputSource; set => _inputSource_field = value; }
        IKeyInputSource _inputSource_field;
        [SerializeField, FormerlySerializedAs("InputSource")] KeyInputSource _inputSource;
        /// <summary>
        /// Map of events to be invoked for specific keys being pressed
        /// </summary>
        public SerializableDictionary<KeyCode, UnityEvent> Events;

        private void Start()
        {
            if (_inputSource.IsNil()) _inputSource_field = IKeyInputSource.Get(this);
        }

        void Update()
        {
            if (InputSource.IsNotNil())
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