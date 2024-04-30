using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarkusSecundus.Utils.Input
{
    public class VirtualJoystickAxis : InputAxisSupplier
    {
        [SerializeField] InputAxisSupplier _movementAxis;
        [SerializeField] float _movementSpeed;

        float _value;
        public override float Value => _value;

        private void Update()
        {
            _value += _movementAxis.Value * _movementSpeed * Time.deltaTime;
            _value = Mathf.Clamp(_value, -1f, 1f);
        }
    }
}
