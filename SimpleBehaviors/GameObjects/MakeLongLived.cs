using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors
{
    public class MakeLongLived : MonoBehaviour
    {
        void Awake()
        {
            Object.DontDestroyOnLoad(this);
        }

    }
}
