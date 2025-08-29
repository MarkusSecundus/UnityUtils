using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Primitives
{
    /// <summary>
    /// Static class containing convenience extensions methods for numeric types.
    /// </summary>
    public static class NumericHelpers
    {
        public static int DivideWithRemainder(this float f, float div, out float remainder)
        {
            double ret = System.Math.Floor((double)f / (double)div);
            remainder = (float)((double)f - (ret * (double)div));
            return (int)ret;
        }

        /// <summary>
        /// Computes division remainder for floats.
        /// 
        /// <para>
        /// Uses the naive - quick but numerically unstable approach.
        /// </para>
        /// </summary>
        /// <param name="f">number to be divided by <paramref name="mod"/></param>
        /// <param name="mod">the divider</param>
        /// <param name="div">result of the division operation</param>
        /// <returns>Remainder after dividing <paramref name="f"/> by <paramref name="mod"/></returns>
        public static float Mod(this float f, float mod, out float div)
        {
            var d = System.Math.Floor(f / mod);
            div = (float)d;
            var ret = f - (d * mod);
            if (ret < 0f) ret += mod;
            return (float)ret;
        }
        public static int Mod(this int i, int mod) => ((i % mod) + mod) % mod;
        /// <summary>
        /// Computes division remainder for floats.
        /// 
        /// <para>
        /// Uses the naive - quick but numerically unstable approach.
        /// </para>
        /// </summary>
        /// <param name="f">number to be divided by <paramref name="mod"/></param>
        /// <param name="mod">the divider</param>
        /// <returns>Remainder after dividing <paramref name="f"/> by <paramref name="mod"/></returns>
        public static float Mod(this float f, float mod) => f.Mod(mod, out _);


		//TODO: check if this is correct!
        public static float DegreesNormalize(this float f) => f.AngleNormalize(180f);
        public static float AngleNormalize(this float f, float halfTurn)
        {
            f = f.Mod(2f*halfTurn);
            if (f > halfTurn)
                f -= 2f*halfTurn;
            return f;
        }

        public static float DegreesSubtract(this float a, float b) => a.AngleSubtract(b, 180f);
        public static float AngleSubtract(this float a, float b, float halfTurn)
        {
            float ret = a - b;
            ret = ret.AngleNormalize(halfTurn);
            return ret;
        }


        /// <summary>
        /// Computes 2nd power of given number (<c><paramref name="x"/> * <paramref name="x"/></c>)
        /// </summary>
        /// <param name="x">Power to be multiplied with itself</param>
        /// <returns><c><paramref name="x"/> * <paramref name="x"/></c></returns>
        public static float Pow2(this float x) => x * x;

        /// <summary>
        /// Check if the number is <c>NaN</c> - fluent shortcut for <see cref="System.Single.IsNaN(float)"/>.
        /// </summary>
        /// <param name="f">number to be checked for <c>NaN</c></param>
        /// <returns><c>true</c> IFF <paramref name="f"/> is <c>NaN</c></returns>
        public static bool IsNaN(this float f) => float.IsNaN(f);
        /// <summary>
        /// Check if the number is <c>PositiveInfinity</c> - fluent shortcut for <see cref="System.Single.IsPositiveInfinity(float)"/>.
        /// </summary>
        /// <param name="f">number to be checked for <c>PositiveInfinity</c></param>
        /// <returns><c>true</c> IFF <paramref name="f"/> is <c>PositiveInfinity</c></returns>
        public static bool IsPositiveInfinity(this float f) => float.IsPositiveInfinity(f);
        /// <summary>
        /// Check if the number is <c>IsNegativeInfinity</c> - fluent shortcut for <see cref="System.Single.IsNegativeInfinity(float)"/>.
        /// </summary>
        /// <param name="f">number to be checked for <c>IsNegativeInfinity</c></param>
        /// <returns><c>true</c> IFF <paramref name="f"/> is <c>IsNegativeInfinity</c></returns>
        public static bool IsNegativeInfinity(this float f) => float.IsNegativeInfinity(f);

        /// <summary>
        /// Check if the number is neither <c>NaN</c>,<c>PositiveInfinity</c> nor <c>IsNegativeInfinity</c>, but just an ordinary finite number.
        /// </summary>
        /// <param name="f">number to be checked</param>
        /// <returns><c>true</c> IFF <paramref name="f"/> is <c>NaN</c>,<c>PositiveInfinity</c> or <c>IsNegativeInfinity</c></returns>
        public static bool IsNormalNumber(this float f) => !f.IsNaN() && !f.IsNegativeInfinity() && !f.IsPositiveInfinity();

        /// <summary>
        /// Checks if given number is really small
        /// </summary>
        /// <param name="f">Number to be compared</param>
        /// <param name="epsilon">Epsilon that defines smallness</param>
        /// <returns><c>true</c> IFF <paramref name="f"/>'s absolute value is smaller than <paramref name="epsilon"/></returns>
        public static bool IsNegligible(this float f, float? epsilon = null) => Mathf.Abs(f) < (epsilon ?? Mathf.Epsilon);
        /// <summary>
        /// Checks if given two numbers are really close to each other
        /// </summary>
        /// <param name="f">Number to be compared</param>
        /// <param name="g">Other number to be compared</param>
        /// <param name="epsilon">Epsilon that defines closeness</param>
        /// <returns><c>true</c> IFF distance between the two numbers is smaller than <paramref name="epsilon"/></returns>
        public static bool IsCloseTo(this float f, float g, float? epsilon = null) => (f - g).IsNegligible(epsilon);

        public static float Clamp(this float f, float max, float min) => Mathf.Clamp(f, max, min);
        public static float Clamp01(this float f) => Mathf.Clamp01(f);

        public static int Sqr(this int x) => x * x;
        public static float Sqr(this float x) => x * x;
        public static double Sqr(this double x) => x * x;
    }
}
