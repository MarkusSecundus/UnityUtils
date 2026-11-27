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
            if (UnityEngine.Input.touchCount <= 0) return;

            var camera = Camera.main;
            if (camera == null || camera.pixelWidth == 0 || camera.pixelHeight == 0)
            {
                Debug.LogWarning($"{nameof(TouchscreenEvent)}: No valid camera to compute screen-space touch coordinates from.", this);
                return;
            }

            var inverseScreenSize = new Vector2(1f / camera.pixelWidth, 1f / camera.pixelHeight);

            for (int t = 0; t < UnityEngine.Input.touchCount; ++t)
            {
                var touch = UnityEngine.Input.GetTouch(t);
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