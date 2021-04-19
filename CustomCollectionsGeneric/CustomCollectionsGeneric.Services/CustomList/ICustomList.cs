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
        public void AddRange(IEnumerable<T> collection);

        public ReadOnlyCollection<T> AsReadOnly();

        public void Clear();

        public bool Contains(T item);
        public void CopyTo(T[] array, int arrayIndex);

        public void CopyTo(T[] array);

        public bool Exists(Predicate<T> match);

        public T? Find(Predicate<T> match);

        public CustomList<T> FindAll(Predicate<T> match);

        public int FindIndex(int startIndex, int count, Predicate<T> match);

        public int FindIndex(Predicate<T> match);
    }
}
