using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using static CustomCollectionsGeneric.Services.Message;

namespace CustomCollectionsGeneric.Services.CustomArray
{
    public class CustomArray<T> : ICustomArray<T>, IEnumerable<T>
    {
        private bool isReadOnly;
        private T[] array = null;
        public int Length => array.Length;
        private int currentIndex;
        private int lastItemIndex;
        private T defaultValue = default(T);

        public CustomArray(int length)
        {
            this.array = new T[length];
            this.isReadOnly = false;
            this.currentIndex = 0;
            this.lastItemIndex = 0;
        }

        public T this[int index]
        {
            get
            {
                CheckForIndexesRange(index);
                return array[index];
            }
            set
            {
                CheckForIndexesRange(index);
                if (isReadOnly)
                    throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
                this.InsertAt(index, value);
            }
        }

        private void InsertAt(int index, T item)
        {
            array[index] = item;
        }
        private void Add(T item)
        {
            if (this.lastItemIndex >= this.Length)
                throw new IndexOutOfRangeException(noMoreSpace + this.GetType().Name);
            InsertAt(lastItemIndex,item);
            lastItemIndex++;
        }


        public ReadOnlyCollection<T> AsReadOnly(T[] array)
        {
            var readOnly = new ReadOnlyCollection<T>(array);
            return readOnly;
        }

        public void AsReadOnly(bool isReadOnly)
        {
            this.isReadOnly = isReadOnly;
        }

        public void Clear(int index, int length)
        {
            CheckForIndexesRange(index);
            CheckForIndexesRange(length);
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            int? countToGet = null;
            if (length >= this.Length)
            {
                countToGet = this.Length - index;
                return;
            }
            int? realLength = countToGet == null ? length : countToGet;
            for (int i = index; i <= realLength; i++)
            {
                this.array[i] = defaultValue;
            }
        }

        public void Clear(int index)
        {
            CheckForIndexesRange(index);
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            for (int i = index; i < this.Length; i++)
            {
                this.array[i] = defaultValue;
            }
        }
        public void Clear()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            this.array = new T[Length];
        }

        private void CheckForIndexesRange(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException(lessThanZero);
            if (index >= Length)
                throw new IndexOutOfRangeException(theGivenParametarWasTooBig);
        }

        public object Clone()
        {
            var newArray = new CustomArray<T>(Length);
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray.Add(this[i]);
            }
            return newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (HasNext())
            {
                yield return this[currentIndex];
                MoveNext();
            }
            Reset();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        private bool HasNext() =>
             this.currentIndex < this.Length;

        public void Reset()
        {
            currentIndex = 0;
        }

        private bool MoveNext()
        {
            if (this.HasNext())
            {
                this.currentIndex++;
                return true;
            }
            return false;
        }

        public void IsReadOnly(bool isReadOnly)
        {
            this.isReadOnly = isReadOnly;
        }
        public CustomArray<T> Empty(int length) => new CustomArray<T>(length);
        public void Fill(T value)
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            for (int i = 0; i < this.Length; i++)
            {
                this.array[i] = value;
            }
        }
        public CustomArray<T> Find(Func<T, bool> predicate)
        {
            var tempArr = array.Where(predicate).ToArray();
            var newlyArray = new CustomArray<T>(tempArr.Length);
            for (int i = 0; i < tempArr.Length; i++)
            {
                newlyArray.Add(tempArr[i]);
            }
            return newlyArray;
        }

        public bool Exists(Func<T, bool> predicate)
        {
            var tempArr = array.Where(predicate).ToArray();
            return tempArr.Length >= 1 ? true : false;
        }

       
    }
}