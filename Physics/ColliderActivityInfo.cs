using MarkusSecundus.Utils.Datastructs;
using MarkusSecundus.Utils.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Physics
{
    public interface IColliderActivityInfo
    {
        public void Enter(Component other);
        public void Exit(Component other);
    }

    public class ColliderActivityInfo<TElement> : IColliderActivityInfo where TElement : class
    {
        public Func<Component, TElement> Extractor;
        public Func<TElement, bool> Validator;
        DefaultValDict<TElement, HashSet<Component>> _active = new(k=>new());
        public IReadOnlyCollection<TElement> Active => ((Dictionary<TElement, HashSet<Component>>)_active.Base).Keys;
        public ColliderActivityInfo(Func<Component, TElement> extractor, Func<TElement, bool> validator = null) => (Extractor, Validator) = (extractor, validator);

        public TElement Enter(Component other)
        {
            var target = Extractor(other);
            if (target.IsNil() || Validator?.Invoke(target) == false)
                return default;
            return _active[target].Add(other) ? target : default;
        }


        public TElement Exit(Component other)
        {
            var target = Extractor(other);
            if (target.IsNotNil()) return default;
            if (!_active.TryGetValue(target, out var list)) return default;
            if (!list.Remove(other) || !list.IsEmpty()) return default;
            _active.Remove(target);
            return target;
        }

        void IColliderActivityInfo.Enter(Component other) => Enter(other);

        void IColliderActivityInfo.Exit(Component other) => Exit(other);
    }
}
