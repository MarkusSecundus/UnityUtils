using MarkusSecundus.Utils.Extensions;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.GameObjects
{
    public class HelperSingleton : MonoBehaviour 
    {
        static HelperSingleton _instance = null;
        public static HelperSingleton Instance { get
            {
                if (!_instance)
                {
                    _instance = new GameObject(nameof(HelperSingleton)).AddComponent<HelperSingleton>();
                }
                return _instance;
            } }
    }

    public class LifetimeHelper : MonoBehaviour
    {
        private void Awake()
        {
            var _ = HelperSingleton.Instance;
        }
        public void DestroyImmediate()
        {
            if(gameObject)
                Destroy(gameObject);
        }
        public void ScheduleDestroy(float untilDestroy)
        {
            if (untilDestroy < -0f) DestroyImmediate();
            HelperSingleton.Instance.InvokeWithDelay(() => { if (gameObject) Destroy(gameObject); }, untilDestroy);
        }
        public void ScheduleDestroyNextFrame()
        {
            HelperSingleton.Instance.InvokeWithDelay(() => { if (gameObject) Destroy(gameObject); }, null);
        }

        public void SwitchActiveness()
        {
            this.gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
