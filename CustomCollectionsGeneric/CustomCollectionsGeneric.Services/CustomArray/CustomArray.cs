using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        /// <summary>
        /// Constructor that initialise a new instance of CustomArray with given length
        /// </summary>
        /// <param name="length">How big to be the array</param>
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

        /// <summary>
        /// Takes an index and item to add. Removes last value on this index
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="item">Item to add</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values</exception>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if the given index is less than zero or bigger than the length of the array</exception>
        private void InsertAt(int index, T item)
        {
            CheckForIndexesRange(index);
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            array[index] = item;
        }

        /// <summary>
        /// Adds the given item in the end of the array
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if array is full</exception>
        private void Add(T item)
        {
            if (this.lastItemIndex >= this.Length)
                throw new IndexOutOfRangeException(noMoreSpace + this.GetType().Name);
            InsertAt(lastItemIndex,item);
            lastItemIndex++;
        }

        /// <summary>
        /// Makes an given array to readonly array
        /// </summary>
        /// <param name="array">Array to make readonly</param>
        /// <returns>Returns the array as readonly</returns>
        /// <exception cref="ArgumentNullException">The error can be thrown if array is null</exception>
        public ReadOnlyCollection<T> AsReadOnly(T[] array)
        {
            var readOnly = new ReadOnlyCollection<T>(array);
            return readOnly;
        }

        /// <summary>
        /// Makes current array readonly or not. If true the array cannot be changed after this only can be read.
        /// </summary>
        /// <param name="isReadOnly">To be readonly or not</param>
        public void AsReadOnly(bool isReadOnly)
        {
            this.isReadOnly = isReadOnly;
        }

        /// <summary>
        /// Receives index(startIndex) and length(including) 
        /// </summary>
        /// <param name="index">From index to start</param>
        /// <param name="length">How long from array you want</param>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if the given index is less than zero or bigger than the length of the array</exception>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values</exception>
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

        /// <summary>
        /// Clears the array from given index till the end
        /// </summary>
        /// <param name="index">From index to start</param>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if the given index is less than zero or bigger than the length of the array</exception>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values</exception>

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
        /// <summary>
        /// Clears the whole array
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values</exception>
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

        /// <summary>
        /// Clones the whole array into new one
        /// </summary>
        /// <returns>Newly array with filled values</returns>
        public CustomArray<T> Clone()
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


        /// <summary>
        /// Makes newly array with default values
        /// </summary>
        /// <param name="length">The length of the array</param>
        /// <returns>Newly array</returns>
        public CustomArray<T> Empty(int length) => new CustomArray<T>(length);

        /// <summary>
        /// Fills the whole array with given value
        /// </summary>
        /// <param name="value">Value to fill with</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values</exception>
        public void Fill(T value)
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            for (int i = 0; i < this.Length; i++)
            {
                this.array[i] = value;
            }
        }
        /// <summary>
        /// Returns newly array with elements that meet a given condition
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>Newly array with elements</returns>
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

        /// <summary>
        /// Search for an element that meet a given condition
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>Returns true if one of all elements meet with the given condition;otherwise false</returns>
        public bool Exists(Func<T, bool> predicate)
        {
            var tempArr = array.Where(predicate).ToArray();
            return tempArr.Length >= 1 ? true : false;
        }
    }
}