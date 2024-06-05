using DG.Tweening;
using MarkusSecundus.Utils.Assets._Scripts.Utils.Datastructs;
using MarkusSecundus.Utils.Datastructs;
using MarkusSecundus.Utils.Extensions;
using MarkusSecundus.Utils.Primitives;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MarkusSecundus.Utils
{
    public class ObjectFollowPath : MonoBehaviour
    {
        [System.Serializable]public struct PathSegment
        {
            public Transform TargetPosition;
            public Ease Ease;
            public float MovementDurationOverride_seconds;
            public bool ShouldOverrideMovementDuration => MovementDurationOverride_seconds > 0f;
            public UnityEvent OnReached;
        }

        [SerializeField] PathSegment[] _path;
        [SerializeField] float _movementSpeed;
        [SerializeField] MovementType _movementType = MovementType.OneShot;

        [System.Serializable]public enum MovementType
        {
            OneShot, Cyclic, CyclicBacktracking
        }
        IEnumerable<int> _getIndexSupplier(MovementType movementType) => movementType switch
        {
            MovementType.OneShot => IntRange.FromZero(_path.Length),
            MovementType.Cyclic => IntRange.FromZero(_path.Length).Repeat(null),
            MovementType.CyclicBacktracking => IntRange.FromZero(_path.Length).Chain(IntRange.BeginCount(_path.Length-1, _path.Length-1, -1)).Repeat(null),
            _ => throw new System.ArgumentOutOfRangeException($"Invalid value {movementType} for enum {nameof(MovementType)}")
        };

        bool _isMoving = false;
        public void StartMovement()
        {
            Debug.Log($"Requested movement start!", this);
            if(Op.post_assign(ref _isMoving, true)) return;
            Debug.Log($"Initiated movement start!", this);

            var indices = _getIndexSupplier(_movementType);
            nextSegment(indices.GetEnumerator());

            void nextSegment(IEnumerator<int> indexSupplier)
            {
                if (!indexSupplier.MoveNext())
                {
                    Debug.Log($"Finished movement!", this);
                    indexSupplier.Dispose();
                    _isMoving = false;
                    return;
                }
                var segment = _path[indexSupplier.Current];
                var distance = transform.position.Distance(segment.TargetPosition.position);
                var movementDuration = segment.ShouldOverrideMovementDuration 
                                        ? segment.MovementDurationOverride_seconds
                                        : distance / _movementSpeed;
                Debug.Log($"Step {indexSupplier.Current} - duration: {movementDuration}", this);
                transform.DOMove(segment.TargetPosition.position, movementDuration).SetEase(segment.Ease).OnComplete(() =>
                {
                    segment.OnReached?.Invoke();
                    nextSegment(indexSupplier);
                });
            }
        }
    }
}
