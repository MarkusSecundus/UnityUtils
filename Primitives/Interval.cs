using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MarkusSecundus.Utils.Primitives
{
    public static class Interval
    {
        public static Interval<Vector3> Make(Vector3 a, Vector3 b) => new Interval<Vector3>(a.Min(b), a.Max(b));

        public static Interval<Vector2Int> Make(Vector2Int a, Vector2Int b) => new Interval<Vector2Int>(a.Min(b), a.Max(b));

        public static float Clamp(this float f, Interval<float> i) => Mathf.Clamp(f, i.Min, i.Max);

        public static bool Contains(this Interval<float> i, float f) => i.Min <= f && f < i.Max;
        public static bool Contains(this Interval<int> i, int t) => i.Min <= t && t < i.Max;

        public static Interval<Vector3> Enlarge(this Interval<Vector3> a , Interval<Vector3> b) => new Interval<Vector3>(new Vector3(Mathf.Min(a.Min.x, b.Min.x), Mathf.Min(a.Min.y, b.Min.y), Mathf.Min(a.Min.z, b.Min.z)), new Vector3(Mathf.Max(a.Max.x, b.Max.x), Mathf.Max(a.Max.y, b.Max.y), Mathf.Max(a.Max.z, b.Max.z)));
        public static Interval<Vector2Int> Enlarge(this Interval<Vector2Int> a , Interval<Vector2Int> b) => new Interval<Vector2Int>(new Vector2Int(Mathf.Min(a.Min.x, b.Min.x), Mathf.Min(a.Min.y, b.Min.y)), new Vector2Int(Mathf.Max(a.Max.x, b.Max.x), Mathf.Max(a.Max.y, b.Max.y)));

        public static float Average(this Interval<float> self) => (self.Min + self.Max) * 0.5f;
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
