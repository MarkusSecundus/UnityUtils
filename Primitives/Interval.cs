using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Primitives
{
    public static class Interval
    {
        public static Interval<Vector3> Make(Vector3 a, Vector3 b) => new Interval<Vector3>(new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z)),new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z)));

        public static float Clamp(this float f, Interval<float> i) => Mathf.Clamp(f, i.Min, i.Max);

        public static bool Contains(this Interval<float> i, float f) => i.Min <= f && f < i.Max;
        public static bool Contains(this Interval<int> i, int t) => i.Min <= t && t < i.Max;
    }

    /// <summary>
    /// Object for defining intervals
    /// </summary>
    /// <typeparam name="T">Used numeric type</typeparam>
    [System.Serializable]
    public struct Interval<T>
    {
        /// <summary>
        /// Construct the interval
        /// </summary>
        /// <param name="min">Lower bound of the interval</param>
        /// <param name="max">Upper bound of the interval</param>
        public Interval(T min, T max) => (Min, Max) = (min, max);

        /// <summary>
        /// Lower bound of the interval
        /// </summary>
        public T Min;
        /// <summary>
        /// Upper bound of the interval
        /// </summary>
        public T Max;
    }

}
