using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.Automatization
{
    /// <summary>
    /// Component that can be used as a source of spawnpoints for spawner mechanisms like <see cref="PlaceObjectsOnPoints"/>.
    /// </summary>
    public abstract class IPointsSupplier : MonoBehaviour
    {
        /// <summary>
        /// Iterate through list of all points of the discretized variant of this shape
        /// </summary>
        /// <returns>Iterator for finite ammount of points</returns>
        public abstract IEnumerable<Vector3> IteratePoints();
        /// <summary>
        /// Get random point in the diameter of the shape, does NOT have to be one exactly obtainable by <see cref="IteratePoints"/>
        /// </summary>
        /// <param name="rand">Source of randomness</param>
        /// <returns>Random point of the shape</returns>
        public abstract Vector3 GetRandomPoint(System.Random rand);
        /// <summary>
        /// Get random point inside the volume of the shape
        /// </summary>
        /// <param name="rand">Source of randomness</param>
        /// <returns>Random point inside the shape's volume</returns>
        public abstract Vector3 GetRandomPointInVolume(System.Random rand);
    }
}