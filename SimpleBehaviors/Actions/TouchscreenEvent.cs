using MarkusSecundus.Utils.Primitives;
using UnityEngine;
using UnityEngine.Events;


namespace MarkusSecundus.Utils.Behaviors.Actions
{
    public class TouchscreenEvent : MonoBehaviour
    {
        [SerializeField]
        EventDescriptor[] Events;
        [System.Serializable]
        public struct EventDescriptor
        {
            public TouchPhase Phase;
            public Rect ScreenRect;
            public UnityEvent Event;
        }


        private void Update()
        {
            if (Input.touchCount <= 0) return;

            var inverseScreenSize = new Vector2(1f / Camera.main.pixelWidth, 1f / Camera.main.pixelHeight);

            for (int t = 0; t < Input.touchCount; ++t)
            {
                var touch = Input.GetTouch(t);
                foreach (var ev in Events)
                {
                    if (touch.phase == ev.Phase && ev.ScreenRect.Contains(touch.position.MultiplyElems(inverseScreenSize)))
                    {
                        ev.Event?.Invoke();
                    }
                }
            }
        }
    }
}