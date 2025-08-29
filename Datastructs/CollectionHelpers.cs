using MarkusSecundus.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace MarkusSecundus.Utils.Datastructs
{
    /// <summary>
    /// Static class containing convenience extensions methods for standard collections
    /// </summary>
    public static class CollectionHelpers
    {
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> self) => self.Count <= 0;

        /// <summary>
        /// Yield given number of results obtained from a given supplier
        /// </summary>
        /// <typeparam name="T">Type of the element</typeparam>
        /// <param name="supplier">Supplier for the iteration elements</param>
        /// <param name="count">How many elements</param>
        /// <returns>Generator that yields elements obtained from <paramref name="supplier"/></returns>
        public static IEnumerable<T> Repeat<T>(this System.Func<T> supplier, int count)
        {
            while (--count >= 0)
                yield return supplier();
        }

        /// <summary>
        /// Get last element of a list
        /// </summary>
        /// <typeparam name="T">Type of list's elements</typeparam>
        /// <param name="self">List to be peeked</param>
        /// <returns>Last element of the list</returns>
        public static T Peek<T>(this IReadOnlyList<T> self) => self[self.Count - 1];
        /// <summary>
        /// Get last element of a list if it has any
        /// </summary>
        /// <typeparam name="T">Type of list's elements</typeparam>
        /// <param name="self">List to be peeked</param>
        /// <param name="ret">Last element of the list or <c>default</c></param>
        /// <returns><c>true</c> if the list is non-empty</returns>
        public static bool TryPeek<T>(this IReadOnlyList<T> self, out T ret)
        {
            if (self.IsNullOrEmpty())
            {
                ret = default;
                return false;
            }
            else
            {
                ret = self.Peek();
                return true;
            }
        }
        /// <summary>
        /// Generator that lazily iterates through given stream given number of times
        /// </summary>
        /// <typeparam name="T">Type of stream's elements</typeparam>
        /// <param name="self">Stream to be iterated multiple times</param>
        /// <param name="repeatCount">How many times to iterate through the stream</param>
        /// <returns>Generator that lazily iterates through given stream given number of times</returns>
        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> self, int? repeatCount)
        {
            while ((repeatCount == null) || (--repeatCount >= 0))
                foreach (var i in self) yield return i;
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> self)
        {
            foreach (var it in self) foreach (var e in it) yield return e;
        }

        public static IEnumerable<T> Chain<T>(this IEnumerable<T> self, params IEnumerable<T>[] others)
        {
            foreach (var e in self) yield return e;
            foreach (var it in others) foreach (var e in it) yield return e;
        }

        public static int RemoveAllDead<T>(this ICollection<T> self, List<object> tempBuffer = null) where T: class
        {
            tempBuffer?.Clear();
            tempBuffer ??= new();
            int ret = 0;
            foreach(var e in self)
                if (e.IsNil()) tempBuffer.Add(e);
            foreach (var e in tempBuffer)
            {
                if (self.Remove((T)e)) ++ret;
            }
            tempBuffer.Clear();
            return ret;
        }

        /// <summary>
        /// Concatenates all elements into a string
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="self">Stream of elements to concatenate into a string</param>
        /// <param name="separator">Separator to be inserted between element's string representations</param>
        /// <returns>String concatenation of all provided elements</returns>
        public static string MakeString<T>(this IEnumerable<T> self, string separator = ", ", string ifNull = "<nil>", string ifEmpty="")
        {
            if (self.IsNil()) return ifNull;
            using var it = self.GetEnumerator();

            if (!it.MoveNext()) return ifEmpty;
            var ret = new StringBuilder().Append(it.Current.ToString());
            while (it.MoveNext()) ret = ret.Append(separator).Append(it.Current.ToString());

            return ret.ToString();
        }

        /// <summary>
        /// If the collection is null or empty
        /// </summary>
        /// <typeparam name="T">Type of elements</typeparam>
        /// <param name="self">Collection to be checked for emptiness</param>
        /// <returns><c>true</c> iff the collection is null or empty</returns>
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> self) => self.IsNil() || self.Count <= 0;

        /// <summary>
        /// Gets smallest value in a stream, using provided selector for comparisons.
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <typeparam name="TComp">Type of the elements used for comparison</typeparam>
        /// <param name="self">Stream to be searched through</param>
        /// <param name="selector">Function for obtaining comparable representative for each element</param>
        /// <returns>Value whose representative was the smallest</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If the provided stream is empty</exception>
        public static T Minimal<T, TComp>(this IEnumerable<T> self, System.Func<T, TComp> selector) where TComp : System.IComparable<TComp>
        {
            using var it = self.GetEnumerator();
            if (!it.MoveNext()) throw new System.ArgumentOutOfRangeException("Empty collection was provided!");

            var ret = it.Current;
            var min = selector(ret);

            while (it.MoveNext())
            {
                var cmp = selector(it.Current);
                if (cmp.CompareTo(min) < 0)
                {
                    min = cmp;
                    ret = it.Current;
                }
            }

            return ret;
        }
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> self) => new Dictionary<TKey, TValue>(self);

        public static T[] Concat<T>(this T[] self, T[] toConcat)
        {
            if (self.IsNullOrEmpty()) return toConcat ?? System.Array.Empty<T>();
            if (toConcat.IsNullOrEmpty()) return self ?? System.Array.Empty<T>();
            T[] ret = new T[self.Length + toConcat.Length];
            System.Array.Copy(self, ret, self.Length);
            System.Array.Copy(toConcat, 0, ret, self.Length, toConcat.Length);
            return ret;
        }
        public static bool TryGetTheOnlyElement<T>(this IEnumerable<T> self, out T ret)
        {
            ret = default;
            using var it = self.GetEnumerator();
            if (!it.MoveNext())
                return false;
            ret = it.Current;
            if (it.MoveNext())
                return false;
            return true;
        }


        public static (string Left, string Right) SplitByFirstOccurence(this string s, string separator)
        {
            var i = s.IndexOf(separator);
            if (i < 0) return (s, null);
            return (s.Substring(0, i), s.Substring(i + separator.Length));
        }


        public struct IndexedEnumerator<TCollection, TItem> : IEnumerator<TItem>, IEnumerator where TCollection : IReadOnlyList<TItem>
        {
            TCollection _base;
            int _currentIndex;
            public IndexedEnumerator(TCollection baseCollection) => (_base, _currentIndex) = (baseCollection, -1);
            public TItem Current { get
                {
                    if (_currentIndex < 0) throw new System.InvalidOperationException($"Reading Current before first calling MoveNext()");
                    return _base[_currentIndex];
                } }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() => (++_currentIndex) < _base.Count;

            public void Reset() => _currentIndex = -1;
        }
    }
}
