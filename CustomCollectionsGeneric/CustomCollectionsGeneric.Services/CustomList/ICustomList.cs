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
        ReadOnlyCollection<T> AsReadOnly();
        bool Exists(Func<T, bool> match);
        void Insert(int index,T item);
        T Find(Func<T, bool> predicate);
        CustomList<T> FindAll(Func<T, bool> predicate);
        void RemoveAt(int index);
        CustomArray<T> ToArray();
        int LastIndexOf(T item);
        bool RemoveAll(T item);

    }
}
