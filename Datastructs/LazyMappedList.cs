using MarkusSecundus.Utils.Behaviors.Automatization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusSecundus.Utils.Datastructs
{
    public class LazyMappedList<TSource, TValue> : IReadOnlyList<TValue>
    {
        public delegate TValue Supplier(TSource sourceValue, int index);
        public IReadOnlyList<TSource> Source { get; }
        Supplier _supplier;
        List<TValue> _data = new();
        public LazyMappedList(IReadOnlyList<TSource> source, Supplier supplier)
            => (Source, _supplier) = (source, supplier);

        public TValue this[int index] { get { _ensureExists(index); return _data[index]; } }

        public int Count => Source.Count;

        public IEnumerator<TValue> GetEnumerator()
        {
            _ensureExists(Source.Count - 1);
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void _ensureExists(int index)
        {
            if (index < _data.Count) return;
            if (index >= Count) throw new System.ArgumentOutOfRangeException($"Index {index} out of range for list of size {Count}");
            _data.Capacity = index + 1;
            for (int t = _data.Count; t <= index; ++t)
                _data.Add(_supplier(Source[t], t));
        }
    }
}
