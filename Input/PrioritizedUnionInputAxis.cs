
using System.Linq;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class PrioritizedUnionInputAxis : InputAxisSupplier
    {
        [SerializeField] InputAxisSupplier[] _axes;

        public override float Value => _axes.FirstOrDefault(a => a.Value != 0f)?.Value ?? 0f;
    }
}
