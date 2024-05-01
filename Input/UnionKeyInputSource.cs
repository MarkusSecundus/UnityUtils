using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class UnionKeyInputSource : KeyInputSource
    {
        [SerializeField] KeyInputSource[] _sources;

        public override bool IsAnyKeyUp => _sources.Any(s => s.IsAnyKeyUp);

        public override bool IsAnyKeyDown => _sources.Any(s => s.IsAnyKeyDown);

        public override bool IsAnyKeyPressed => _sources.Any(s => s.IsAnyKeyPressed);

        public override bool GetKeyDown(KeyCode key) => _sources.Any(s => s.GetKeyDown(key));

        public override bool GetKey(KeyCode key) => _sources.Any(s => s.GetKey(key));

        public override bool GetKeyUp(KeyCode key) => _sources.Any(s => s.GetKeyUp(key));
    }
}
