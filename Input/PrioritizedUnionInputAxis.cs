
using System.Linq;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class PrioritizedUnionInputAxis : InputAxisSupplier
    {
        [SerializeField] InputAxisSupplier[] _axes;

        int _activeAxisIndex = -1;
        private void Awake()
        {
            _activeAxisIndex = _axes.Length - 1;
        }
        public override float Value { get
            {
                if (_activeAxisIndex < 0) return 0f;
                for (int t = 0; t < _activeAxisIndex; ++t)
                {
                    var axis = _axes[t].Value;
                    if(axis != 0)
                    {
                        _activeAxisIndex = t;
                        return axis;
                    }
                }
                return _axes[_activeAxisIndex].Value;
            } }

    }
}
