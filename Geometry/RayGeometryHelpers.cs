using MarkusSecundus.Utils.Primitives;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Geometry
{
    /// <summary>
    /// Static class providing methods for performing geometric computations on rays
    /// </summary>
    public static class RayGeometryHelpers
    {
        static double GetRayPointWithLeastDistance_GetParameter(this ScaledRay self, Vector3 v)
        {
            return -Vector3.Dot((self.origin - v), self.direction) / self.direction.magnitude.Pow2();
        }

        /// <summary>
        /// Project given point on a given ray.
        /// </summary>
        /// <param name="self">Ray on which the projection lies.</param>
        /// <param name="v">Point to project</param>
        /// <returns>Projection of <paramref name="v"/> on <paramref name="self"/></returns>
        public static Vector3 GetRayPointWithLeastDistance(this ScaledRay self, Vector3 v)
            => self.GetPoint(self.GetRayPointWithLeastDistance_GetParameter(v));


        /// <summary>
        /// Get point going along the ray.
        /// </summary>
        /// <param name="self">Ray on which the point lies</param>
        /// <param name="t">Distance to travel from origin</param>
        /// <returns><c>self.origin + t*self.direction</c></returns>
        public static Vector3 GetPoint(this ScaledRay self, double t)
            => self.origin + (float)t * self.direction;
    }
}
