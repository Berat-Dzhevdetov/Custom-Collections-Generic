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
        /// Initialize new <typeparamref name="CustomList"/> with default values.
        /// </summary>
        public CustomList()
        {
            this.array = new CustomArray<T>(defaultCapacity);
            isReadOnly = false;
        }

        /// <summary>
        /// Initialize new <typeparamref name="CustomArray"/> and add values from array to the List.
        /// </summary>
        /// <param name="collection">Values to add as an array.</param>
        public CustomList(ICustomArray<T> collection)
            : this()
        {
            this.AddRange(collection);
        }
        /// <summary>
        /// Initialize new <typeparamref name="CustomList"/> and add values from List to the new List.
        /// </summary>
        /// <param name="collection">Values to add as a List.</param>
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
        /// Receives an <typeparamref name="Item"/> and adds it to the end of the <typeparamref name="CustomList"/>.
        /// </summary>
        /// <param name="item"><typeparamref name="Item"/> to add.</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Add(T item)
        {
            IsReadOnly();
            if (Count >= Capacity)
                array.Resize(Capacity * defaultScale);
            array[Count] = item;
            Count++;
        }
        /// <summary>
        /// Takes an array and adds the values ​​of the array to the list.
        /// </summary>
        /// <param name="collection">Аn array from which it takes values.</param>
        public void AddRange(ICustomArray<T> collection)
        {
            foreach (var item in (CustomArray<T>)collection)
            {
                Add(item);
            }
        }
        /// <summary>
        /// Gets an List and adds the values ​​of the List to the list.
        /// </summary>
        /// <param name="collection">Аn List from which it takes values.</param>
        public void AddRange(ICustomList<T> collection)
        {
            foreach (var item in (CustomList<T>)collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Makes the List as a ReadOnlyCollection.
        /// </summary>
        /// <returns>List its values ​​as <typeparamref name="ReadOnlyCollection"/> of type <typeparamref name="T"/>.</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return array.AsReadOnly();
        }
        public void IsReadOnly(bool state)
        {
            isReadOnly = state;
        }
        /// <summary>
        /// Remove the items from the list.
        /// </summary>
        public void Clear()
        {
            array = new CustomArray<T>(defaultCapacity);
            Count = 0;
        }
        /// <summary>
        /// Checks for given item if it is in the CustomList<T>/>.
        /// </summary>
        /// <param name="item"><paramref name="item"/> to find</param>
        /// <returns><paramref name="true"/> if <paramref name="item"/> was found,otherwise <paramref name="false"/>.</returns>
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

        /// <summary>
        /// Checks for item that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns><paramref name="true"/> if <paramref name="item"/> was found, otherwise <paramref name="false"/>.</returns>
        public bool Exists(Func<T, bool> predicate)
        {
            return array.Exists(predicate);
        }
        /// <summary>
        /// Checks for item that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns>Returns first found item, if the item is not found will return default value of <typeparamref name="T"/></returns>
        public T Find(Func<T, bool> predicate)
        {
            return array.Find(predicate);
        }
        /// <summary>
        /// Checks for item that meet a given condition.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns><paramref name="CustomList"/> with elemets that meet the given condition.</returns>
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

        /// <summary>
        /// Receives an <paramref name="item"/> and search for it in the array.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>If the <paramref name="item"/> was found will return his zero-based index, otherwise will return -1.</returns>
        public int IndexOf(T item)
        {
            if (!Contains(item))
                return -1;
            for (int i = 0; i < Count; i++)
                if (item.Equals(array[i]))
                    return i;
            return -1;
        }
        /// <summary>
        /// Receives <paramref name="index"/> and <typeparamref name="T"/> <paramref name="item"/> and insert it on the given index
        /// </summary>
        /// <param name="index">Zero-based index to insert item in</param>
        /// <param name="item">Item to insert</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
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

        /// <summary>
        /// Receives <typeparamref name="T"/> <paramref name="item"/> and looking for it in the List
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns zero-based index </returns>
        public int LastIndexOf(T item)
        {
            if (!Contains(item))
                return -1;
            for (int i = Count - 1; i >= 0; i--)
                if (item.Equals(array[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// If the List contains the given <paramref name="item"/> it removes it.
        /// </summary>
        /// <param name="item"><paramref name="item"/> to remove</param>
        /// <returns><paramref name="true"/> if item was removed, otherwise will return <paramref name="false"/>.</returns>
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

        /// <summary>
        /// If the List contains the given <paramref name="item"/> one or more times it will remove all of them.
        /// </summary>
        /// <param name="item"><paramref name="item"/> to remove</param>
        /// <returns><paramref name="true"/> if the item was removed, otherwise <paramref name="false"/>.</returns>
        public bool RemoveAll(T item)
        {
            var didReturnSomething = false;
            while (Remove(item))
                didReturnSomething = true;
            return didReturnSomething;
        }

        /// <summary>
        /// Will remove the item on zero-based given index.
        /// </summary>
        /// <param name="index">Zero-based index to remove at.</param>
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

        /// <summary>
        /// Reverses the <typeparamref name="CustomList"/>
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Reverse()
        {
            IsReadOnly();
            if (!Any())
                return;
            var newArray = new CustomArray<T>(Capacity);
            int counter = 0;
            for (int i = this.Count - 1; i >= 0; i--, counter++)
            {
                newArray[counter] = array[i];
            }
            this.array = newArray;
        }

        /// <summary>
        /// Sort the <typeparamref name="CustomList"/> ascending.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
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

        /// <summary>
        /// Sort the <typeparamref name="CustomList"/> descending.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void SortDescending()
        {
            IsReadOnly();
            if (!Any())
                return;
            Sort();
            array.Reverse();
        }

        /// <summary>
        /// Makes a new <typeparamref name="CustomArray"/> with the values of <typeparamref name="CustomList"/>.
        /// </summary>
        /// <returns><typeparamref name="CustomArray"/> with the values.</returns>
        public CustomArray<T> ToArray()
        {
            var newlyArray = new CustomArray<T>(this.Count);
            for (int i = 0; i < Count; i++)
                newlyArray[i] = array[i];
            return newlyArray;
        }

        /// <summary>
        /// Checks if there is anything in the <typeparamref name="CustomList"/>.
        /// </summary>
        /// <returns><paramref name="true"/> if there is something in the <typeparamref name="CustomList"/>, otherwise <paramref name="false"/></returns>
        public bool Any() => this.Count >= 1 ? true : false;

        /// <summary>
        /// Checks if there is anything in the <typeparamref name="CustomList"/> that meets the given conditions.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Checks if List is read only.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
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