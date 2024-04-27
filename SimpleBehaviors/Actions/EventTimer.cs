using MarkusSecundus.Utils.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils.Behaviors.Actions
{
    /// <summary>
    /// Simple component for invoking an action with delay.
    /// </summary>
    public class EventTimer : MonoBehaviour
    {
        /// <summary>
        /// Event to invoke
        /// </summary>
        public UnityEvent ToRun;
        /// <summary>
        /// How many seconds from timer's start until the action is invoked
        /// </summary>
        public float DelaySeconds = 1f;
        /// <summary>
        /// Whether the time should start on the Start() signal
        /// </summary>
        public bool OnStartup = false;

        // Start is called before the first frame update
        void Start()
        {
            if (OnStartup) StartTimer();
        }

        /// <summary>
        /// Start the timer manully
        /// </summary>
        public void StartTimer()
        {
            this.InvokeWithDelay(ToRun.Invoke, DelaySeconds);
        }
    }
}