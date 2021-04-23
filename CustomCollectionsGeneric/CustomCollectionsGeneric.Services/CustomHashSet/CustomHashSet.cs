using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollectionsGeneric.Services.CustomHashSet
{
    public class CustomHashSet<T> : ICustomCollection<T>,ICustomHashSet<T>,IEnumerable<T>
    {
        public int Count => list.Count;
        private CustomList<T> list;
        private int currentIndex;

        public CustomHashSet()
        {
            list = new CustomList<T>();
        }

        public CustomHashSet(ICustomArray<T> array)
        {
            AddRange(array);
        }

        /// <summary>
        /// Receives an <typeparamref name="Item"/> and adds it to the end of the <typeparamref name="CustomHastSet"/> only if there is no item like it.
        /// </summary>
        /// <param name="item"><typeparamref name="Item"/> to add.</param>
        public bool Add(T item)
        {
            if (Contains(item))
                return false;
            list.Add(item);
            return true;
        }
        /// <summary>
        /// Takes an array and adds the values ​​of the array to the list if there is no item like it.
        /// </summary>
        /// <param name="collection">Аn array from which it takes values.</param>
        public void AddRange(ICustomArray<T> collection)
        {
            foreach (var item in (CustomArray<T>)collection)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Gets an List and adds the values ​​of the List to the list if there is no item like it.
        /// </summary>
        /// <param name="collection">Аn List from which it takes values.</param>
        public void AddRange(ICustomList<T> collection)
        {
            foreach (var item in (CustomList<T>)collection)
            {
                list.Add(item);
            }
        }
        /// <summary>
        /// Checks if there is anything in the <typeparamref name="CustomHashSet"/>.
        /// </summary>
        /// <returns><paramref name="true"/> if there is something in the <typeparamref name="CustomHashSet"/>, otherwise <paramref name="false"/></returns>
        public bool Any() => list.Any();

        /// <summary>
        /// Checks if there is anything in the <typeparamref name="CustomHashSet"/> that meets the given conditions.
        /// </summary>
        /// <param name="predicate">Defines the conditions of the element to search for.</param>
        /// <returns><paramref name="true"/> if item was found,otherwise <paramref name="false"/></returns>
        public bool Any(Func<T, bool> predicate)
         => list.Any(predicate);

        /// <summary>
        /// Remove the items from the <typeparamref name="CustomHashSet"/>.
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }

        /// <summary>
        /// Checks for given item if it is in the <typeparamref name="CustomHashSet"/>.
        /// </summary>
        /// <param name="item"><paramref name="item"/> to find</param>
        /// <returns><paramref name="true"/> if <paramref name="item"/> was found,otherwise <paramref name="false"/>.</returns>
        public bool Contains(T item) =>
            list.Contains(item);

        /// <summary>
        /// Copies the whole <typeparamref name="CustomHashSet"/> into given <typeparamref name="CustomArray"/>. If there is data in the array it can be lost
        /// </summary>
        /// <param name="array">Array to set values</param>
        public void CopyTo(out CustomArray<T> array)
        {
            list.CopyTo(out array);
        }

        /// <summary>
        /// Copies the <typeparamref name="CustomHashSet"/> into <typeparamref name="CustomArray"/> from given index.
        /// </summary>
        /// <param name="array">Array to fill with the values.</param>
        /// <param name="startIndex">Zero-based index.</param>
        /// <exception cref="ArgumentException">It can be thrown if given index was out of range.</exception>
        public void CopyTo(out CustomArray<T> array, int startIndex)
        {
            list.CopyTo(out array, startIndex);
        }

        /// <summary>
        /// Receives an <paramref name="item"/> and search for it in the <typeparamref name="CustomHashSet"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>If the <paramref name="item"/> was found will return his zero-based index, otherwise will return -1.</returns>
        public int IndexOf(T item) => list.IndexOf(item);

        /// <summary>
        /// Receives <paramref name="index"/> and <typeparamref name="T"/> <paramref name="item"/> and insert it on the given index
        /// </summary>
        /// <param name="index">Zero-based index to insert item in</param>
        /// <param name="item">Item to insert</param>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        /// <summary>
        /// Receives <typeparamref name="T"/> <paramref name="item"/> and looking for it in the <typeparamref name="CustomHashSet"/>
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>Zero-based index if item was found, otherwise -1.</returns>
        public int LastIndexOf(T item) =>
            list.LastIndexOf(item);

        /// <summary>
        /// If the <typeparamref name="CustomHashSet"/> contains the given <paramref name="item"/> it removes it.
        /// </summary>
        /// <param name="item"><paramref name="item"/> to remove.</param>
        /// <returns><paramref name="true"/> if item was removed, otherwise will return <paramref name="false"/>.</returns>
        public bool Remove(T item) =>
            list.Remove(item);

        /// <summary>
        /// If the <typeparamref name="CustomHashSet"/> contains the given <paramref name="item"/> one or more times it will remove all of them.
        /// </summary>
        /// <param name="item"><paramref name="item"/> to remove.</param>
        /// <returns><paramref name="true"/> if the item was removed, otherwise <paramref name="false"/>.</returns>
        public bool RemoveAll(T item) => list.RemoveAll(item);

        /// <summary>
        /// Reverses the <typeparamref name="CustomHashSet"/>
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Reverse()
        {
            list.Reverse();
        }

        /// <summary>
        /// Sort the <typeparamref name="CustomHashSet"/> ascending.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void Sort()
        {
            list.Sort();
        }

        /// <summary>
        /// Sort the <typeparamref name="CustomHashSet"/> descending.
        /// </summary>
        /// <exception cref="FieldAccessException">The error can be thrown if array is marked as read only and someone tries to edit the values.</exception>
        public void SortDescending()
        {
            list.SortDescending();
        }
        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            while (HasNext())
            {
                yield return list[currentIndex];
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
