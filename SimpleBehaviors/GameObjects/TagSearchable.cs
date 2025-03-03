using MarkusSecundus.Utils.Datastructs;
using MarkusSecundus.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.GameObjects
{
    [DefaultExecutionOrder(-1000)]
    public class TagSearchable : MonoBehaviour
    {
        static DefaultValDict<string, List<GameObject>> _values = new(k => new());

        public static GameObject FindByTag(string tag)
        {
            if (!_values.TryGetValue(tag, out var list) || list.IsNullOrEmpty())
                return null;
            for(; list.Count > 0;)
            {
                if (list[list.Count-1]) return list[list.Count - 1];
                else list.RemoveAt(list.Count-1);
            }
            return null;
        }
        public static TComponent FindByTag<TComponent>(string tag) where TComponent : Component
        {
            if (!_values.TryGetValue(tag, out var list) || list.IsNullOrEmpty())
                return null;
            for (; list.Count > 0;)
            {
                if (list[list.Count - 1])
                {
                    var ret = list[list.Count - 1].GetComponent<TComponent>();
                    if (ret) return ret;
                }
                else list.RemoveAt(list.Count - 1);
            }
            return null;
        }

        public static IEnumerable<GameObject> FindAllByTag(string tag)
        {
            if (!_values.TryGetValue(tag, out var list) || list.IsNullOrEmpty())
                return null;
            return list;
        }

        public static IEnumerable<TComponent> FindAllByTag<TComponent>(string tag) where TComponent : Component
        {
            return FindAllByTag(tag)?.Select(o => o.GetComponents<TComponent>())?.Flatten();
        }

        protected virtual void Awake()
        {
            if (gameObject.tag == null) throw new System.Exception($"Object {name} claims to be tag-searchable, but has no tag!");
            if (GetComponent<TagSearchable>() != this) throw new System.Exception($"More than one {nameof(TagSearchable)} components on object {name}!");
            _values[gameObject.tag].Add(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (gameObject.tag == null) return;
            if(_values.TryGetValue(gameObject.tag, out var list))
            {
                list.Remove(gameObject);
                if (list.Count <= 0) _values.Remove(gameObject.tag);
            }
        }
    }
}
