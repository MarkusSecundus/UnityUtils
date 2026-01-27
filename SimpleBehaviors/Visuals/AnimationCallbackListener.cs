using MarkusSecundus.Utils.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils.Behaviors.Cosmetics
{
	public class AnimationCallbackListener : MonoBehaviour
	{
		[SerializeField] SerializableDictionary<string, UnityEvent> Events;
		[SerializeField] UnityEvent FallbackEvent;

		public void Event(string name)
		{
			if(Events.TryGetValue(name, out var ev))
			{
				ev.Invoke();
			}
			else
			{
				FallbackEvent.Invoke();
			}
		}
	}
}
