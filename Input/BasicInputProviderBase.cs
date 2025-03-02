using MarkusSecundus.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public abstract class BasicInputProviderBase<TInputAxis> : AbstractInputProvider<TInputAxis>
    {
        protected abstract string GetAxisName(TInputAxis axis);

        [SerializeField] Camera _camera;
        new Camera camera => _camera = _camera.IfNil(Camera.main);

        public override float GetAxis(TInputAxis axis) => UnityEngine.Input.GetAxis(GetAxisName(axis));
        public override float GetAxisRaw(TInputAxis axis) => UnityEngine.Input.GetAxisRaw(GetAxisName(axis));

        public override bool GetKey(KeyCode c) => UnityEngine.Input.GetKey(c);

        public override bool GetKeyDown(KeyCode c) => UnityEngine.Input.GetKeyDown(c);

        public override bool GetKeyUp(KeyCode c) => UnityEngine.Input.GetKeyUp(c);

        public override Ray GetMouseRay()
        {
            var ret = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            //Debug.DrawRay(ret.origin, ret.direction*10f, Color.yellow);
            return ret;
        }
    }
}