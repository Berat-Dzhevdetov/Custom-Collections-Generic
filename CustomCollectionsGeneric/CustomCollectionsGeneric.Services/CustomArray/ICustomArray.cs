using CustomCollectionsGeneric.Services.CustomList;
using System;
using System.Collections.ObjectModel;

namespace CustomCollectionsGeneric.Services.CustomArray
{
    public interface ICustomArray<T>
    {
        void IsReadOnly(bool isReadOnly);

        ReadOnlyCollection<T> AsReadOnly();
        void Clear();
        void Clear(int index);
        void Clear(int index, int length);
        CustomArray<T> Clone();
        CustomArray<T> Empty(int length);
        void Fill(T value);

        CustomArray<T> FindAll(Func<T, bool> predicate);
        T Find(Func<T, bool> predicate);
        T FindLast(Func<T, bool> predicate);
        bool Exists(Func<T, bool> predicate);

        int IndexOf(T index);
        int LastIndexOf(T item);
        void Resize(int newLength);
        void Reverse();
        void Sort();
        void SortDescending();
        bool Any();
        bool Any(Func<T, bool> predicate);
        ICustomList<T> ToList();
    }
}
