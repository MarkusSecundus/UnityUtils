using MarkusSecundus.Utils.Datastructs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

namespace MarkusSecundus.Utils.Assets._Scripts.Utils.Datastructs
{
    public class IntRange : IReadOnlyList<int>
    {
        public IntRange(int begin, int count, int increment)
        {
            (Begin, Count, Increment) = (begin, count, increment);
            if (Count < 0) throw new System.ArgumentOutOfRangeException($"Cannot construct {nameof(IntRange)} with negative length!");
        }

        public static IntRange BeginEnd(int begin, int endExclusive, int increment = 1)
        {
            if (begin == endExclusive) return new IntRange(begin, 0, increment);
            if (increment == 0) 
                throw new System.ArgumentException($"Cannot construct {nameof(IntRange)} with increment 0 going from {begin} to {endExclusive}");

            var distance = endExclusive - begin;

            if (Math.Sign(distance) != Math.Sign(increment))
                throw new System.ArgumentException($"Cannot construct {nameof(IntRange)} with increment 0 going from {begin} to {endExclusive}");
            return new IntRange(begin, distance / increment, increment);
        }
        public static IntRange BeginCount(int begin, int count, int increment = 1) => new IntRange(begin, count, increment);
        public static IntRange FromZero(int count, int increment = 1) => new IntRange(0, count, increment);

        public int Begin { get; }
        public int Count { get; }
        public int Increment { get; }

        public int this[int index] => (index >= Count || index < 0)? throw new System.ArgumentOutOfRangeException($"Index {index} out of range for {nameof(IntRange)} of length {Count}")
                                        : Begin + index * Increment;

        public CollectionHelpers.IndexedEnumerator<IntRange, int> GetEnumerator() => new (this);

        IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"Range[{Begin}..{this[^1]}]";
    }
}
