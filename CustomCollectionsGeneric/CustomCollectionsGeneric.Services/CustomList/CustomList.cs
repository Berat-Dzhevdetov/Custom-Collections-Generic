using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollectionsGeneric.Services.CustomList
{
    public class CustomList<T> : ICustomList<T>
    {
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Predicate<T> match)
        {
            throw new NotImplementedException();
        }

        public T? Find(Predicate<T> match)
        {
            throw new NotImplementedException();
        }

        public CustomList<T> FindAll(Predicate<T> match)
        {
            throw new NotImplementedException();
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            throw new NotImplementedException();
        }

        public int FindIndex(Predicate<T> match)
        {
            throw new NotImplementedException();
        }
    }
}
