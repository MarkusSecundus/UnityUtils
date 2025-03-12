using MarkusSecundus.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils
{

    [DefaultExecutionOrder(-10)]
    public class PlaceOnGround : MonoBehaviour
    {
        [SerializeField] Transform _pivot;
        [SerializeField] Vector3 _raycastDirection = Vector3.down;
        [SerializeField] float _maxPlacementDistance = 10f;

        private void Start()
        {
            if (!_pivot) _pivot = this.transform;
            MoveToGround();
        }

        void MoveToGround()
        {
            if (UnityEngine.Physics.Raycast(_pivot.position, _raycastDirection, out var hitInfo, _maxPlacementDistance)){
                Vector3 offset = hitInfo.point - _pivot.position;
                this.transform.position += offset;
            }
        }
    }
}
