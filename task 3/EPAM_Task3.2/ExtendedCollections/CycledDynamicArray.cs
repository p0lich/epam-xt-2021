using System;
using System.Collections.Generic;

namespace ExtendedCollections
{
    public class CycledDynamicArray<T> : DynamicArray<T>
    {
        public CycledDynamicArray() : base() { }
        public CycledDynamicArray(int length) : base(length) { }
        public CycledDynamicArray(IEnumerable<T> collection) : base(collection) { }

        public override IEnumerator<T> GetEnumerator()
        {
            int i = 0;

            while (true)
            {
                yield return _data[i % Length];
                i++;
            }
        }
    }
}
