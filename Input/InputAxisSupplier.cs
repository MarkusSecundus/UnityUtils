using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public interface IInputAxisSupplier
    {
        public float Value { get; }
    }
    public abstract class InputAxisSupplier : MonoBehaviour, IInputAxisSupplier
    {
        public abstract float Value { get; }
    }
}
