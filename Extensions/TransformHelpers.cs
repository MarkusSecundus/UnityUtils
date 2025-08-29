using MarkusSecundus.Utils.Primitives;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Extensions
{
    /// <summary>
    /// Static class containing convenience extensions methods for <see cref="Transform"/>
    /// </summary>
    public static class TransformHelpers
    {
        /// <summary>
        /// Get position of <paramref name="self"/> in coordinates relative to <paramref name="relativeTo"/>
        /// </summary>
        /// <param name="self"><c>this</c> component</param>
        /// <param name="relativeTo">Transform determining the coordinate space</param>
        /// <returns>Position of <paramref name="self"/> relative to <paramref name="relativeTo"/></returns>
        public static Vector3 GetPositionRelativeTo(this Transform self, Transform relativeTo)
            => (relativeTo == self) ? Vector3.zero :
               (relativeTo == self.parent) ? self.localPosition :
                  relativeTo.GlobalToLocal(self.position);
        /// <summary>
        /// Sets position of <paramref name="self"/> in coordinates relative to <paramref name="relativeTo"/>
        /// </summary>
        /// <param name="self"><c>this</c> component</param>
        /// <param name="relativeTo">Transform determining the coordinate space</param>
        /// <param name="position">Position to set, in coordinate space relative to <paramref name="relativeTo"/></param>
        /// <returns>Set position in world space</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Vector3 SetPositionRelativeTo(this Transform self, Transform relativeTo, Vector3 position)
        {
            if (relativeTo == self)
                return (position == Vector3.zero) ? position : throw new System.ArgumentException("Position relative to self cannot be anything other than zero", nameof(relativeTo));
            if (relativeTo == self.parent) return self.localPosition = position;
            return self.position = relativeTo.LocalToGlobal(position);
        }

        public static void SnapWithPivot(this Transform self, Transform pivot, Vector3 targetPosition)
        {
            self.position += (targetPosition - pivot.position);
        }

        /// <summary>
        /// Transform a point from local space to world space. Same as <see cref="Transform.TransformPoint(Vector3)"/> but with more readable name.
        /// </summary>
        /// <param name="self">Transform determining the local space</param>
        /// <param name="v">Vector to be transformed</param>
        /// <returns>Value of <paramref name="v"/> in  world space</returns>
        public static Vector3 LocalToGlobal(this Transform self, Vector3 v) => self.TransformPoint(v);
        /// <summary>
        /// Transform a point from world space to local space. Same as <see cref="Transform.InverseTransformPoint(Vector3)"/> but with more readable name.
        /// </summary>
        /// <param name="self">Transform determining the local space</param>
        /// <param name="v">Vector to be transformed</param>
        /// <returns>Value of <paramref name="v"/> in space local to <paramref name="self"/></returns>
        public static Vector3 GlobalToLocal(this Transform self, Vector3 v) => self.InverseTransformPoint(v);
        /// <summary>
        /// Transform a ray from local space to world space. Same as transforming both its points with <see cref="Transform.TransformPoint(Vector3)"/> but with more readable name.
        /// </summary>
        /// <param name="self">Transform determining the local space</param>
        /// <param name="r">Ray to be transformed</param>
        /// <returns>Value of <paramref name="r"/> in world space</returns>
        public static ScaledRay LocalToGlobal(this Transform self, ScaledRay r) => r.GenericTransform(self.LocalToGlobal);
        /// <summary>
        /// Transform a ray from world space to local space. Same as transforming both its points with <see cref="Transform.InverseTransformPoint(Vector3)"/> but with more readable name.
        /// </summary>
        /// <param name="self">Transform determining the local space</param>
        /// <param name="r">Ray to be transformed</param>
        /// <returns>Value of <paramref name="r"/> in space local to <paramref name="self"/></returns>
        public static ScaledRay GlobalToLocal(this Transform self, ScaledRay r) => r.GenericTransform(self.GlobalToLocal);


        [System.Serializable]
        public struct TransformSnapshot
        {
            public Vector3 LocalPosition;
            public Quaternion LocalRotation;
            public Vector3 LocalScale;
        }

        public static TransformSnapshot MakeSnapshot(this Transform t) => new TransformSnapshot { LocalPosition = t.localPosition, LocalRotation = t.localRotation, LocalScale = t.localScale };

        public static void SetFromSnapshot(this Transform t, TransformSnapshot s)
        {
            t.localPosition = s.LocalPosition;
            t.localRotation = s.LocalRotation;
            t.localScale = s.LocalScale;
        }

        static ScaledRay GenericTransform(this ScaledRay r, System.Func<Vector3, Vector3> transformPoints)
        {
            Vector3 a = transformPoints(r.origin), b = transformPoints(r.origin + r.direction);
            return ScaledRay.FromPoints(a, b);
        }

        /// <summary>
        /// Computes worldspace length of a chain of transforms (distance traveled when going through all their points)
        /// </summary>
        /// <param name="self">Chain of transforms</param>
        /// <returns>Total length of travel through all the transforms</returns>
        public static double ComputeChainLength(this IEnumerable<Transform> self)
        {
            double ret = 0f;

            Transform last = null;
            foreach (var t in self)
            {
                if (last != null)
                    ret += t.position.Distance(last.position);
                last = t;
            }

            return ret;
        }

		// TODO: check if this function really is correct!
        public static void ScaleAroundPoint(this Transform self, Vector3 newLocalScale, Vector3 pointWorldspace)
        {
            var pointLocal = self.GlobalToLocal(pointWorldspace);
            self.localScale = newLocalScale;
            var pointNewWorldspace = self.LocalToGlobal(pointLocal);
            var shift = pointNewWorldspace - pointWorldspace;
            self.position -= shift;
        }
    }
}
