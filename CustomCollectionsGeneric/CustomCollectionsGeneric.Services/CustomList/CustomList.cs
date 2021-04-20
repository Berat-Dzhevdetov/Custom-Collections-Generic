using CustomCollectionsGeneric.Services.CustomArray;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomCollectionsGeneric.Services.Message;

namespace CustomCollectionsGeneric.Services.CustomList
{
    public class CustomList<T> : ICustomList<T>
    {
        private const int defaultScale = 2;
        private const int defaultCapacity = 2;
        public int Count { get; private set; }
        private CustomArray<T> array;
        public int Capacity => array.Length;
        private bool isReadOnly;

        public CustomList()
        {
            this.array = new CustomArray<T>(defaultCapacity);
            isReadOnly = false;
        }

        public CustomList(ICustomArray<T> collection)
            : this()
        {
            this.AddRange(collection);
        }


        public void Add(T item)
        {
            if (Count >= Capacity)
            {
                array.Resize(Capacity * defaultScale);
            }
            array[Count] = item;
            Count++;
        }
        public void AddRange(ICustomArray<T> collection)
        {
            foreach (var item in (CustomArray<T>)collection)
            {
                Add(item);
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return array.AsReadOnly();
        }

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

        public void CopyTo(ICustomArray<T> array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(ICustomArray<T> array, int arrayIndex, int count)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(ICustomArray<T> array)
        {
            throw new NotImplementedException();
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

        public int FindIndex(int startIndex, int count, Func<T, bool> match)
        {
            throw new NotImplementedException();
        }

        public int FindIndex(Func<T, bool> match)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public int LastIndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Contains(item))
                return false;
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(item))
                {
                    for (int j = i; j < Count; j++)
                    {
                        array[j] = array[j + 1]; 
                    }
                    Count--;
                    if (Capacity / 2 == Count && Count > 2)
                    {
                        array.Resize(Capacity / 2 /2);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAll(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Reverse()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            array.Reverse();
        }

        public void Sort()
        {
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
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
            if (isReadOnly)
                throw new FieldAccessException(cannotAccessWhileArrayIsReadOnly);
            if (!Any())
                return;
            Sort();
            array.Reverse();
        }

        public CustomArray<T> ToArray()
        {
            var newlyArray = new CustomArray<T>(this.Count);
            for (int i = 0; i < Count; i++)
            {
                newlyArray[i] = array[i];
            }
            return newlyArray;
        }
        public bool Any() => this.Count >= 1 ? true : false;

        public bool Any(Func<T, bool> predicate) => array.Any(predicate);
    }
}
