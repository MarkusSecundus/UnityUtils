using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    internal class DummyInputAxis : InputAxisSupplier
    {
        [field: SerializeField] public override float Value { get;}
    }
}
