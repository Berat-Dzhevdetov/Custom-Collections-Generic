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
        public CustomArray<T> Clone();
        public CustomArray<T> Empty(int length);
        public void Fill(T value);

        public CustomArray<T> Find(Func<T, bool> predicate);

        public bool Exists(Func<T, bool> predicate);
    }
}
