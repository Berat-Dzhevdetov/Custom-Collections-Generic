using CustomCollectionsGeneric.Services.CustomList;
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
        private T defaultValue = default(T);
        public bool IsFixedSize = true;

        public int Rank => 1;

        /// <summary>
        /// Constructor that initialise a new instance of CustomArray with given length.
        /// </summary>
        /// <param name="length">How big to be the array.</param>
        public CustomArray(int length)
        {
            this.array = new T[length];
            this.isReadOnly = false;
            this.currentIndex = 0;
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
        /// Takes an index and item to add. Removes last value on this index.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <param name="item">Item to add.</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if the given index is less than zero or bigger than the length of the array.</exception>
        private void InsertAt(int index, T item)
        {
            if (!Any())
                return;
            CheckForIndexesRange(index);
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            array[index] = item;
        }

        /// <summary>
        /// Makes an given array to readonly array.
        /// </summary>
        /// <param name="array">Array to make readonly<./param>
        /// <returns>Returns the array as readonly.</returns>
        /// <exception cref="ArgumentNullException">The error can be thrown if array is null.</exception>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            var readOnly = new ReadOnlyCollection<T>(this.array);
            return readOnly;
        }

        /// <summary>
        /// Makes current array readonly or not. If true the array cannot be changed after this only can be read.
        /// </summary>
        /// <param name="isReadOnly">To be readonly or not.</param>
        public void IsReadOnly(bool isReadOnly)
        {
            this.isReadOnly = isReadOnly;
        }

        /// <summary>
        /// Receives index(startIndex) and length(including).
        /// </summary>
        /// <param name="index">From index to start.</param>
        /// <param name="length">How long from array you want.</param>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if the given index is less than zero or bigger than the length of the array.</exception>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Clear(int index, int length)
        {
            if (!Any())
                return;
            CheckForIndexesRange(index);
            CheckForIndexesRange(length);
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            int? countToGet = null;
            if (index + length >= this.Length)
            {
                countToGet = this.Length - 1;
            }
            int? realLength = countToGet == null ? length : countToGet;
            for (int i = index; i <= realLength; i++)
            {
                this.array[i] = defaultValue;
            }
        }

        /// <summary>
        /// Clears the array from given index till the end.
        /// </summary>
        /// <param name="index">From index to start.</param>
        /// <exception cref="IndexOutOfRangeException">The error can be thrown if the given index is less than zero or bigger than the length of the array.</exception>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>

        public void Clear(int index)
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            CheckForIndexesRange(index);
            for (int i = index; i < this.Length; i++)
            {
                this.array[i] = defaultValue;
            }
        }
        /// <summary>
        /// Clears the whole array.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Clear()
        //I think it's better to create newly array instead of 
        //foreaching all elements and sets their value to default.
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
        /// Clones the whole array into new one.
        /// </summary>
        /// <returns>Newly array with filled values.</returns>
        public CustomArray<T> Clone()
        {
            if (!Any())
                return new CustomArray<T>(Length);
            var newArray = new CustomArray<T>(Length);
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        private T[] Clone(int newSize)
        {
            if (!Any())
                return new T[Length];
            var newArray = new T[newSize];
            for (int i = 0; i < array.Length; i++)
            {
                if (i >= newSize) break;
                newArray[i] = array[i];
            }
            return newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            while (HasNext())
            {
                yield return this[currentIndex];
                MoveNext();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        private bool HasNext() =>
             this.currentIndex < this.Length;

        private void Reset()
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

        /// <summary>
        /// Makes newly array with default values.
        /// </summary>
        /// <param name="length">The length of the array.</param>
        /// <returns>Newly array.</returns>
        public CustomArray<T> Empty(int length)
        {

            if (length <= 0)
                throw new ArgumentException(cannotCreateEmptyArray);
            return new CustomArray<T>(length);
        }

        /// <summary>
        /// Fills the whole array with given value.
        /// </summary>
        /// <param name="value">Value to fill with.</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Fill(T value)
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            for (int i = 0; i < this.Length; i++)
            {
                this.array[i] = value;
            }
        }
        /// <summary>
        /// Returns newly array with elements that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns>Newly array with elements.</returns>
        public CustomArray<T> FindAll(Func<T, bool> predicate)
        {
            if (!Any())
                return new CustomArray<T>(0);
            var tempArr = array.Where(predicate).ToArray();
            var newlyArray = new CustomArray<T>(tempArr.Length);
            for (int i = 0; i < tempArr.Length; i++)
            {
                newlyArray[i] = tempArr[i];
            }
            return newlyArray;
        }
        /// <summary>
        /// Checks for item that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns>Returns first found element, if the item is not found will return default value of <code>T</code></returns>
        public T Find(Func<T, bool> predicate)
        {
            return array.FirstOrDefault(predicate);
        }
        /// <summary>
        /// Search for an element that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns>Returns true if one of all elements meet with the given condition, otherwise false.</returns>
        public bool Exists(Func<T, bool> predicate)
        {
            if (!Any())
                return false;
            var tempArr = array.Where(predicate).ToArray();
            return tempArr.Length >= 1 ? true : false;
        }

        /// <summary>
        /// Search for an element that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns>Last found item or first.</returns>
        public T FindLast(Func<T, bool> predicate)
        {
            return array.LastOrDefault(predicate);
        }
        /// <summary>
        /// Trying to look for given item.
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>If item is found it will return his index; otherwise will return -1.</returns>
        public int IndexOf(T item)
        {
            if (!Any())
                return -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (item.Equals(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Trying to look for given item reversed.
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>If item is found it will return his index; otherwise will return -1 or if array is empty.</returns>
        public int LastIndexOf(T item)
        {
            if (!Any())
                return -1;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (item.Equals(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Makes new array with the given length and copies values.
        /// </summary>
        /// <param name="newLength">Length of new array.</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Resize(int newLength)
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            this.array = Clone(newLength);
        }
        /// <summary>
        /// Make the array reverse. If the array is empty will do nothing.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Reverse()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            var newArray = new T[Length];
            int counter = 0;
            for (int i = array.Length - 1; i >= 0; i--,counter++)
            {
                newArray[counter] = array[i];
            }
            this.array = newArray;
        }
        /// <summary>
        /// Sorts the array in ascending order. If the array is empty will do nothing .
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Sort()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            this.array = array.OrderBy(x => x).ToArray();
        }
        /// <summary>
        /// Sorts the array in descending order. If the array is empty will do nothing.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void SortDescending()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            this.array = array.OrderByDescending(x => x).ToArray();
        }

        /// <summary>
        /// Will check if there is element inside the array.
        /// </summary>
        /// <returns>True if there is a element;otherwise false.</returns>
        public bool Any() => this.Length >= 1 ? true : false;

        /// <summary>
        /// Search if there is any element with given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns></returns>
        public bool Any(Func<T, bool> predicate) => array.Any(predicate);
        /// <summary>
        /// Make your array as list
        /// </summary>
        /// <returns>Returns your array as list</returns>
        public CustomList<T> ToList()
        {
            var list = new CustomList<T>();
            foreach (var item in array)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
