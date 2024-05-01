using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class StandardInputAxis : InputAxisSupplier
    {
        [SerializeField] string Axis;
        [SerializeField] bool UseRaw;
        public override float Value => (string.IsNullOrWhiteSpace(Axis))
                                        ? 0f
                                        : (UseRaw)
                                            ? UnityEngine.Input.GetAxisRaw(Axis) 
                                            : UnityEngine.Input.GetAxis(Axis);
    }
}
