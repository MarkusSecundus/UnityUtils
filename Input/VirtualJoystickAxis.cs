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
        [SerializeField] float _movementSpeed = 1f;
        [SerializeField] AnimationCurve _pushback = AnimationCurve.EaseInOut(-1f, -0.1f, 1f, 0.1f);
        [SerializeField] InputAxisSupplier _toEqualizeAgainst;
        [SerializeField] AnimationCurve _equalizationIntensity = AnimationCurve.Linear(-1f, -0.1f, 1f, 0.1f);

        float _value;
        public override float Value => _value;

        private void Update()
        {
            _value += _movementAxis.Value * _movementSpeed * Time.deltaTime;

            var pushback = _pushback.Evaluate(_value);
            if (_toEqualizeAgainst)
            {
                var equalizationCoefficient = _toEqualizeAgainst.Value - _value;
                var equalization = _equalizationIntensity.Evaluate(equalizationCoefficient);
                _value += equalization * Time.deltaTime;
            }
            _value += pushback * -Mathf.Sign(_value) * Time.deltaTime;


            _value = Mathf.Clamp(_value, -1f, 1f);
        }
    }
}
