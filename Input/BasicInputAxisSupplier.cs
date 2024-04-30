using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class BasicInputAxisSupplier : InputAxisSupplier
    {
        [SerializeField] string Axis;
        [SerializeField] bool UseRaw;
        public override float Value => UseRaw ? UnityEngine.Input.GetAxisRaw(Axis) : UnityEngine.Input.GetAxis(Axis);
    }
}
