using CustomCollectionsGeneric.Services.CustomArray;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static CustomCollectionsGeneric.Services.Message;

namespace CustomCollectionsGeneric.Services.CustomList
{
    public class CustomList<T> : ICustomList<T>, IEnumerable<T>
    {
        private const int defaultScale = 2;
        private const int defaultCapacity = 2;
        public int Count { get; private set; }
        private CustomArray<T> array;
        public int Capacity => array.Length;
        private bool isReadOnly;
        private T defaultValue = default(T);
        private int currentIndex = 0;

        /// <summary>
        /// Initialize new List<T> with default values
        /// </summary>
        public CustomList()
        {
            this.array = new CustomArray<T>(defaultCapacity);
            isReadOnly = false;
        }

        /// <summary>
        /// Initialize new List<T> and add values from array to the List
        /// </summary>
        /// <param name="collection">Values to add as an array</param>
        public CustomList(ICustomArray<T> collection)
            : this()
        {
            this.AddRange(collection);
        }
        /// <summary>
        /// Initialize new List<T> and add values from List to the new List
        /// </summary>
        /// <param name="collection">Values to add as a List</param>
        public CustomList(ICustomList<T> collection)
            : this()
        {
            this.AddRange(collection);
        }


        public T this[int index]
        {
            get
            {
                IsIndexValid(index);
                return array[index];
            }
            set
            {
                IsIndexValid(index);
                IsReadOnly();
                this.Insert(index, value);
            }
        }

        /// <summary>
        /// Receives an element and adds it to the end of the List
        /// </summary>
        /// <param name="item">Element to add</param>
        public void Add(T item)
        {
            if (Count >= Capacity)
                array.Resize(Capacity * defaultScale);
            array[Count] = item;
            Count++;
        }
        /// <summary>
        /// Gets an array and adds the values ​​of the array to the list
        /// </summary>
        /// <param name="collection">Аn array from which it takes values</param>
        public void AddRange(ICustomArray<T> collection)
        {
            foreach (var item in (CustomArray<T>)collection)
            {
                Add(item);
            }
        }
        /// <summary>
        /// Gets an List and adds the values ​​of the List to the list
        /// </summary>
        /// <param name="collection">Аn List from which it takes values</param>
        public void AddRange(ICustomList<T> collection)
        {
            foreach (var item in (CustomList<T>)collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Makes the List as a ReadOnlyCollection
        /// </summary>
        /// <returns>List its values ​​as ReadOnlyCollection</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return array.AsReadOnly();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            array = new CustomArray<T>(defaultCapacity);
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (item.Equals(array[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Exists(Func<T, bool> predicate)
        {
            return array.Exists(predicate);
        }

        public T Find(Func<T, bool> predicate)
        {
            return array.Find(predicate);
        }

        public CustomList<T> FindAll(Func<T, bool> predicate)
        {
            if (!Any())
                return new CustomList<T>();
            var tempArr = array.Where(predicate).ToArray();
            var newlyArray = new CustomList<T>();
            for (int i = 0; i < tempArr.Length; i++)
            {
                newlyArray.Add(tempArr[i]);
            }
            return newlyArray;
        }

        public int IndexOf(T item)
        {
            if (!Contains(item))
                return -1;
            for (int i = 0; i < Count; i++)
                if (item.Equals(array[i]))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            IsReadOnly();
            IsIndexValid(index);
            Count++;
            if (Count >= Capacity)
                array.Resize(Capacity * defaultScale);
            for (int i = Count - 2; i >= index; i--)
            {
                array[i + 1] = array[i];
            }
            array[index] = item;
        }

        public int LastIndexOf(T item)
        {
            if (!Contains(item))
                return -1;
            for (int i = Count - 1; i >= 0; i--)
                if (item.Equals(array[i]))
                    return i;
            return -1;
        }

        public bool Remove(T item)
        {
            IsReadOnly();
            if (!Contains(item))
                return false;
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(item))
                {
                    if (IsLastElement(i))
                        array[i] = defaultValue;
                    else
                    {
                        for (int j = i; j < Count; j++)
                        {
                            if (IsLastElement(j))
                            {
                                array[j] = defaultValue;
                                break;
                            }
                            array[j] = array[j + 1];
                        }
                    }
                    Count--;
                    if (Capacity / 2 == Count && Count > 2)
                        array.Resize(Capacity / 2);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAll(T item)
        {
            var didReturnSomething = false;
            while (Remove(item))
                didReturnSomething = true;
            return didReturnSomething;
        }

        public void RemoveAt(int index)
        {
            IsReadOnly();
            IsIndexValid(index);
            if (IsLastElement(index))
            {
                array[index] = defaultValue;
            }
            else
            {
                for (int j = index; j < Count; j++)
                {
                    if (IsLastElement(j))
                    {
                        array[j] = defaultValue;
                        break;
                    }

                    array[j] = array[j + 1];
                }
            }
            Count--;
            if (Capacity / 2 == Count && Count > 2)
            {
                array.Resize(Capacity / 2);
            }
        }

        public void Reverse()
        {
            IsReadOnly();
            if (!Any())
                return;
            array.Reverse();
        }

        public void Sort()
        {
            IsReadOnly();
            if (!Any())
                return;
            CustomArray<T> a = new CustomArray<T>(Count);
            for (int i = 0; i < Count; i++)
            {
                a[i] = array[i];
            }
            a.Sort();
            this.AddRange(a);
        }

        public void SortDescending()
        {
            IsReadOnly();
            if (!Any())
                return;
            Sort();
            array.Reverse();
        }

        public CustomArray<T> ToArray()
        {
            var newlyArray = new CustomArray<T>(this.Count);
            for (int i = 0; i < Count; i++)
                newlyArray[i] = array[i];
            return newlyArray;
        }

        public bool Any() => this.Count >= 1 ? true : false;

        public bool Any(Func<T, bool> predicate) => array.Any(predicate);

        private bool IsLastElement(int index)
        {
            if (index == Count - 1)
                return true;
            return false;
        }
        private void IsIndexValid(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException(lessThanZero);
            if (index >= Count)
                throw new IndexOutOfRangeException(givenParametarWasOutOfRange);
        }
        private void IsReadOnly()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
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
             this.currentIndex < this.Count;

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
    }
}