using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarkusSecundus.Utils.Datastructs
{
    [System.Serializable]public struct Array2D<T>
    {
        [field: SerializeField]public T[] BackingArray { get; }
        [field: SerializeField] public int Width { get; }
        public int Height => BackingArray.Length / Width;

        public ref T this[int x, int y] => ref BackingArray[y*Width + x];

        public Array2D(T[] baseArray, int width)
        {
            if (baseArray.Length % width != 0)
                throw new System.ArgumentException($"Cannot create 2D array - length {baseArray.Length} of base array is not divisible by width {width}");
            (BackingArray, Width) = (baseArray, width);
        }
        public Array2D(int width, int height) : this(new T[width*height], width) { }


    }

    public static class Array2DHelpers
    {
        public static IEnumerable<IEnumerable<T>> IterateLines<T>(this Array2D<T> self)
        {
            for (int y = 0; y < self.Height; ++y) yield return line(y);

            IEnumerable<T> line(int y)
            {
                for (int x = 0; x < self.Width; ++x)
                    yield return self[x, y];
            }
        }
    }
}
