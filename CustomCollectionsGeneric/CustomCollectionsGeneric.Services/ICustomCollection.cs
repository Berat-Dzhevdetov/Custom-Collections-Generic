using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using System;

namespace CustomCollectionsGeneric.Services
{
    public interface ICustomCollection<T>
    {
        void Clear();
        bool Contains(T item);
        void AddRange(ICustomArray<T> collection);
        void AddRange(ICustomList<T> collection);
        bool Remove(T item);
        bool RemoveAll(T item);
        void Reverse();
        void Sort();
        void SortDescending();
        int LastIndexOf(T item);
        int IndexOf(T item);
        void Insert(int index, T item);
        bool Any();
        bool Any(Func<T, bool> predicate);
    }
}
