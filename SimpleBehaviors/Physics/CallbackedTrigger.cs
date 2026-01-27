using MarkusSecundus.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;
using callback = UnityEngine.Events.UnityAction<UnityEngine.Collider>;


namespace MarkusSecundus.Utils.Behaviors.Physics
{
    /// <summary>
    /// Component that provides callbacks to be invoked on <c>OnTriggerEnter</c> and <c>OnTriggerExit</c> signals.
    /// </summary>
    public class CallbackedTrigger : MonoBehaviour
    {
        public bool ListenToTriggerEvents = true;
        public bool ListenToCollisionEvents = false;

        public string[] TagWhitelist;


        /// <summary>
        /// Action to be invoked when <c>OnTriggerEnter</c> message is received
        /// </summary>
        public UnityEvent<Collider> OnEnter = new UnityEvent<Collider>();

        /// <summary>
        /// Action to be invoked when <c>OnTriggerEnter2D</c> message is received
        /// </summary>
        public UnityEvent<Collider2D> OnEnter2D = new UnityEvent<Collider2D>();
        /// <summary>
        /// Action to be invoked when <c>OnTriggerExit</c> message is received
        /// </summary>
        public UnityEvent<Collider> OnExit = new UnityEvent<Collider>();
        /// <summary>
        /// Action to be invoked when <c>OnTriggerExit2D</c> message is received
        /// </summary>
        public UnityEvent<Collider2D> OnExit2D = new UnityEvent<Collider2D>();

        bool _isInWhitelist(Component com) => (TagWhitelist.Length <= 0) || TagWhitelist.Contains(com.tag);

        void OnTriggerEnter(Collider other)
        {
            if(ListenToTriggerEvents && _isInWhitelist(other)) OnEnter?.Invoke(other);
        }
        void OnTriggerExit(Collider other)
        {
            if(ListenToTriggerEvents && _isInWhitelist(other)) OnExit?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(ListenToCollisionEvents && _isInWhitelist(collision.collider)) OnEnter?.Invoke(collision.collider);
        }
        private void OnCollisionExit(Collision collision)
        {
            if(ListenToCollisionEvents && _isInWhitelist(collision.collider)) OnExit?.Invoke(collision.collider);
        }

		private void OnTriggerEnter2D(Collider2D other)
		{
            if (ListenToTriggerEvents && _isInWhitelist(other)) OnEnter2D?.Invoke(other);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (ListenToTriggerEvents && _isInWhitelist(other)) OnExit2D?.Invoke(other);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (ListenToCollisionEvents && _isInWhitelist(collision.collider)) OnEnter2D?.Invoke(collision.collider);
		}
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (ListenToCollisionEvents && _isInWhitelist(collision.collider)) OnExit2D?.Invoke(collision.collider);

		}
	}
}