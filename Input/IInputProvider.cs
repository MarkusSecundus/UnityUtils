using MarkusSecundus.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace MarkusSecundus.Utils.Input
{
    public interface IInputProvider<TInputAxis> : IKeyInputSource
    {
        public float GetAxis(TInputAxis axis);
        public float GetAxisRaw(TInputAxis axis);

        public Ray GetMouseRay();

        // already present from IKeyInputSource
        //public bool GetKey(KeyCode c);
        //public bool GetKeyDown(KeyCode c);
        //public bool GetKeyUp(KeyCode c);


        public static new IInputProvider<TInputAxis> Get(Component o) => o.GetComponentInParent<IInputProvider<TInputAxis>>();
    }

    public abstract class AbstractInputProvider<TInputAxis> : MonoBehaviour, IInputProvider<TInputAxis>
    {
        public bool IsAnyKeyUp => EnumHelpers.GetValues<KeyCode>().Any(GetKeyUp);

        public bool IsAnyKeyDown => EnumHelpers.GetValues<KeyCode>().Any(GetKeyDown);

        public bool IsAnyKeyPressed => EnumHelpers.GetValues<KeyCode>().Any(GetKey);

        public abstract float GetAxis(TInputAxis axis);
        public abstract float GetAxisRaw(TInputAxis axis);
        public abstract bool GetKey(KeyCode c);
        public abstract bool GetKeyDown(KeyCode c);

        public abstract bool GetKeyUp(KeyCode c);

        public abstract Ray GetMouseRay();
    }
}