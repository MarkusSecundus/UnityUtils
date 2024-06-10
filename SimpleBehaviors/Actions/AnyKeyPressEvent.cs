using MarkusSecundus.Utils.Input;
using MarkusSecundus.Utils.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils.Behaviors.Actions
{
    public class AnyKeyPressEvent : MonoBehaviour
    {
        public KeyInputSource InputSource;

        public UnityEvent OnAnyKeyDown;

        void Update()
        {
            if (InputSource)
            {
                if (InputSource.IsAnyKeyDown) OnAnyKeyDown?.Invoke();
            }
            else
            {
                if (UnityEngine.Input.anyKeyDown) OnAnyKeyDown?.Invoke();
            }
        }
    }
}
