using MarkusSecundus.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class StandardKeyInputSource : KeyInputSource
    {
        public override bool IsAnyKeyUp => EnumHelpers.GetValues<KeyCode>().Any(UnityEngine.Input.GetKeyUp);

        public override bool IsAnyKeyDown => UnityEngine.Input.anyKeyDown;

        public override bool IsAnyKeyPressed => UnityEngine.Input.anyKey;

        public override bool GetKeyDown(KeyCode key) => UnityEngine.Input.GetKeyDown(key);

        public override bool GetKey(KeyCode key) => UnityEngine.Input.GetKey(key);

        public override bool GetKeyUp(KeyCode key) => UnityEngine.Input.GetKeyUp(key);
    }
}
