using MarkusSecundus.Utils.Primitives;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils
{
    [DefaultExecutionOrder(1000)]
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] Transform _target;

        [SerializeField] public Vector3 MovementSpeed = Vector3Int.one;
        //[SerializeField] Vector3SerializableSwizzle _swizzle = Vector3SerializableSwizzle.Default;

        Vector3 _originalPosition, _originalTargetPosition;
        Vector3 _lastTargetPosition;
        private void Start()
        {
            _originalPosition = transform.position;
            _originalTargetPosition = _target.transform.position;
            _lastTargetPosition = _target.transform.position;
        }

        private void LateUpdate()
        {
            //var targetShift = _target.position - _originalTargetPosition;
            //var selfShift = targetShift.MultiplyElems(_movementSpeed);
            //transform.position = _originalPosition + selfShift;

            var delta = _target.transform.position - _lastTargetPosition;
            _lastTargetPosition = _target.transform.position;
            var selfShift = delta.MultiplyElems(MovementSpeed);
            transform.position += selfShift;
        }
    }
}
