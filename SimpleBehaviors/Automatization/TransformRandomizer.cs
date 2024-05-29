using MarkusSecundus.Utils.Primitives;
using MarkusSecundus.Utils.Randomness;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.Automatization
{
    /// <summary>
    /// Provides common functionality for randomizing <see cref="Transform"/> parameters
    /// </summary>
    public class TransformRandomizer : MonoBehaviour, IRandomizer
    {
        /// <summary>
        /// Range for the scale to be applied. X value will be used for Z as well to ensure symmetry
        /// </summary>
        public Interval<Vector3> Scale = new Interval<Vector3>(Vector3.one, Vector3.one);
        public Vector3SerializableSwizzle ScaleSwizzle = new Vector3SerializableSwizzle { X = Vector3SerializableSwizzle.SwizzleOption.X, Y = Vector3SerializableSwizzle.SwizzleOption.Y, Z = Vector3SerializableSwizzle.SwizzleOption.X };
        /// <summary>
        /// Range for the rotation to be applied. Euler angles in degrees.
        /// </summary>
        public Interval<Vector3> Rotation = new Interval<Vector3>(Vector3.zero, new Vector3(0, 360f, 0));
        /// <summary>
        /// Distance in world space by which to offset the current position.
        /// </summary>
        public Interval<Vector3> PlaceOffset = new Interval<Vector3>(Vector3.zero, Vector3.zero);

        /// <inheritdoc/>
        public void Randomize(System.Random random)
        {
            var scale = random.Next(Scale).With(ScaleSwizzle);
            var rotation = random.Next(Rotation);

            transform.localScale = transform.localScale.MultiplyElems(scale);
            transform.localRotation *= Quaternion.Euler(rotation);
            transform.position += random.Next(PlaceOffset);
        }
    }
}