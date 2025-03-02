using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public interface IKeyInputSource
    {
        public bool GetKeyDown(KeyCode key);
        public bool GetKeyUp(KeyCode key);
        public bool GetKey(KeyCode key);

        public bool IsAnyKeyUp { get; }
        public bool IsAnyKeyDown { get; }
        public bool IsAnyKeyPressed { get; }

        public static IKeyInputSource Get(Component o) => o.GetComponentInParent<IKeyInputSource>();
    }
    public abstract class KeyInputSource : MonoBehaviour, IKeyInputSource
    {
        public abstract bool IsAnyKeyUp { get; }
        public abstract bool IsAnyKeyDown { get; }
        public abstract bool IsAnyKeyPressed { get; }
        public abstract bool GetKeyDown(KeyCode key);
        public abstract bool GetKey(KeyCode key);
        public abstract bool GetKeyUp(KeyCode key);
    }
}
