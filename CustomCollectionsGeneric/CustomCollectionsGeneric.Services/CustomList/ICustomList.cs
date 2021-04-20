using CustomCollectionsGeneric.Services.CustomArray;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollectionsGeneric.Services.CustomList
{
    public interface ICustomList<T>
    {
        void Add(T item);
        void AddRange(ICustomArray<T> collection);
        ReadOnlyCollection<T> AsReadOnly();

        void Clear();

        bool Contains(T item);

        void CopyTo(ICustomArray<T> array, int arrayIndex);
        void CopyTo(ICustomArray<T> array, int arrayIndex, int count);

        void CopyTo(ICustomArray<T> array);

        bool Exists(Func<T, bool> match);

        T Find(Func<T, bool> predicate);

        CustomList<T> FindAll(Func<T, bool> predicate);

        int FindIndex(int startIndex, int count, Func<T, bool> predicate);

        int FindIndex(Func<T, bool> match);

        bool Remove(T item);
        bool RemoveAll(T item);
        void RemoveAt(int index);
        void Reverse();
        void Sort();
        void SortDescending();
        CustomArray<T> ToArray();
        int LastIndexOf(T item);
        int IndexOf(T item);
        void Insert(int index,T item);
        bool Any();
        bool Any(Func<T, bool> predicate);
    }
}
