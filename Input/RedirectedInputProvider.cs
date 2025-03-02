using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MarkusSecundus.Utils.Input
{
    public abstract class AbstractRedirectedInputProvider<TInputAxis> : AbstractInputProvider<TInputAxis>
    {
        protected abstract IInputProvider<TInputAxis> _source { get; }

        public override float GetAxis(TInputAxis axis) => _source?.GetAxis(axis) ?? 0f;

        public override float GetAxisRaw(TInputAxis axis) => _source?.GetAxis(axis) ?? 0f;

        public override bool GetKey(KeyCode c) => _source?.GetKey(c) ?? false;

        public override bool GetKeyDown(KeyCode c) => _source?.GetKeyDown(c) ?? false;

        public override bool GetKeyUp(KeyCode c) => _source?.GetKeyUp(c) ?? false;

        public override Ray GetMouseRay() => _source?.GetMouseRay() ?? default;
    }


    public class RedirectedInputProvider<TInputAxis> : AbstractRedirectedInputProvider<TInputAxis>
    {
        [SerializeField] AbstractInputProvider<TInputAxis> _sourceObject;
        protected override IInputProvider<TInputAxis> _source => _sourceObject;
    }
}