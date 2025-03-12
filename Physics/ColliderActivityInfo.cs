using MarkusSecundus.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Physics
{
    public interface IColliderActivityInfo
    {
        public void Enter(Collider other);
        public void Exit(Collider other);
    }

    public class ColliderActivityInfo<TElement> : IColliderActivityInfo where TElement : class
    {
        public Func<Collider, TElement> Extractor;
        public Func<TElement, bool> Validator;
        public HashSet<TElement> Active;

        public ColliderActivityInfo(Func<Collider, TElement> extractor, Func<TElement, bool> validator) => (Extractor, Validator, Active) = (extractor, validator, new());

        public TElement Enter(Collider other)
        {
            var target = Extractor(other);
            if (target.IsNil() || Validator?.Invoke(target) == false)
                return default;
            return Active.Add(target) ? target : default;
        }
        public TElement Exit(Collider other)
        {
            var target = Extractor(other);
            return target.IsNotNil() && Active.Remove(target) ? target : default;
        }

        void IColliderActivityInfo.Enter(Collider other) => Enter(other);

        void IColliderActivityInfo.Exit(Collider other) => Exit(other);
    }
}
