using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtendedCollections
{
    public class DynamicArray<T> : IEnumerable<T>, IEnumerable
    {
        private T[] _data;

        public int Length { get; private set; }

        public int Capacity
        {
            get
            {
                return _data.Length;
            }
        }

        public DynamicArray()
        {
            _data = new T[8];
            Length = 0;
        }

        public DynamicArray(int length)
        {
            _data = new T[length];
            Length = 0;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            RewriteData(ref _data, out int length, collection);
            Length = length;
        }

        public T this[int index]
        {
            // TODO: ArgumentOutOfRangeException
            get
            {
                if (index >= 0)
                {
                    return _data[index];
                }

                return _data[Length + index];
            }
        }

        public void Add(T value)
        {
            if (Length == Capacity)
            {
                IncreaseCapacity();
            }

            _data[Length] = value;
            Length++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            int newLength = GetCollectionLength(collection) + Length;

            if (newLength > Capacity)
            {
                IncreaseCapacity(newLength);
            }

            foreach (var item in collection)
            {
                _data[Length] = item;
                Length++;
            }
        }

        public bool Remove(int index)
        {
            if (index < Length)
            {
                ArrayOffset(index, false);

                _data[Length - 1] = default;
                Length--;

                return true;
            }

            return false;
        }

        public bool Insert(int index, T value)
        {
            // TODO: ArgumentOutOfRangeException

            if (index <= Length)
            {
                if (Length == Capacity)
                {
                    IncreaseCapacity();
                }

                Length++;
                ArrayOffset(index, true);
                _data[index] = value;
                

                return true;
            }

            return false;
        }

        private static int GetCollectionLength(IEnumerable<T> collection)
        {
            int length = 0;

            foreach (var item in collection)
            {
                length++;
            }

            return length;
        }

        private void IncreaseCapacity(int collectionSize)
        {
            //while (Capacity < collectionSize)
            //{
            //    IncreaseCapacity();
            //}

            T[] tempData = _data;
            _data = new T[collectionSize];

            for (int i = 0; i < tempData.Length; i++)
            {
                _data[i] = tempData[i];
            }
        }

        private void IncreaseCapacity()
        {
            T[] tempData = _data;
            _data = new T[Capacity * 2];

            for (int i = 0; i < tempData.Length; i++)
            {
                _data[i] = tempData[i];
            }
        }

        private static void RewriteData(ref T[] data, out int length, IEnumerable<T> collection)
        {
            length = 0;
            List<T> tempData = new List<T>();

            foreach (var item in collection)
            {
                length++;
                tempData.Add(item);
            }

            data = tempData.ToArray();
        }

        private void ArrayOffset(int referenceIndex, bool isAdd)
        {
            if (isAdd)
            {
                for (int i = Length - 2; i >= referenceIndex; i--)
                {
                    _data[i + 1] = _data[i];
                }
            }
            
            else
            {
                for (int i = referenceIndex; i < Length - 1; i++)
                {
                    _data[i] = _data[i + 1];
                }
            }
        }

        // TODO
        #region INTERFACE_IMPLEMENTATION

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
