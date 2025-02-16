using MarkusSecundus.Utils.Primitives;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace MarkusSecundus.Utils.Geometry
{
    /// <summary>
    /// Static class providing methods for performing geometric computations on rectangles
    /// </summary>
    public static class RectGeometryHelpers
    {
        /// <summary>
        /// Compute rectangle describing set of all points where placing a bigger rect covers the whole of a smaller rect.
        /// </summary>
        /// <param name="biggerOne">Rect that is in all dimensions greater than <paramref name="smallerRect"/></param>
        /// <param name="smallerRect">Rect that is in all dimensions smaller than <paramref name="biggerOne"/></param>
        /// <returns></returns>
        public static Rect PositionsWherePlacingThisRectCoversTheWholeOfSmallerRect(this Rect biggerOne, Rect smallerRect)
        {
            Vector2 min = smallerRect.max - biggerOne.size / 2f, max = smallerRect.min + biggerOne.size / 2f;
            return ShapeHelpers.RectFromPoints(min, max);
        }


        // TODO: Properly test that it works and if it does (as it seems so far), figure out why!
        public static Vector2 IntersectWithRay(this Rect self, Vector2 origin, Vector2 direction)
        {
            if (!(self.min.LessThanAll(origin) && origin.LessThanAll(self.max)))
                throw new System.NotImplementedException("Computing intersection for a ray originating outside the rectangle is not implemented!");

            var t  = computeIntersection(origin, direction, self.xMin, out var intersection);
            var t2 = computeIntersection(origin, direction, self.xMax, out var intersection2);
            var t3 = computeIntersection(origin.yx(), direction.yx(), self.yMin, out var intersection3);
            var t4 = computeIntersection(origin.yx(), direction.yx(), self.yMax, out var intersection4);
            if (t2 < t) (t, intersection) = (t2, intersection2);
            if (t3 < t) (t, intersection) = (t3, intersection3.yx());
            if (t4 < t) (t, intersection) = (t4, intersection4.yx());
            
            return intersection;

            float computeIntersection(Vector2 origin, Vector2 direction, float beginX, out Vector2 intersection)
            {
                if(direction.x == 0){
                    intersection = default;
                    return float.PositiveInfinity;
                }

                var xDistance = beginX - origin.x;
                var t = Mathf.Abs(xDistance / direction.x); //TODO: figure out why Abs() needs to be here
                intersection = origin + direction * t ;
                return t;
            }
        }
    }
}
