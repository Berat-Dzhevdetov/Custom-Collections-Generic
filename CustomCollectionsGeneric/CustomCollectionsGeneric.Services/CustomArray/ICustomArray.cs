using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollectionsGeneric.Services.CustomArray
{
    public interface ICustomArray<T>
    {
        void IsReadOnly(bool isReadOnly);

        ReadOnlyCollection<T> AsReadOnly(T[] array);
        void Clear();
        void Clear(int index);
        void Clear(int index, int length);
        public object Clone();
        public CustomArray<T> Empty(int length);
        public void Fill(T value);

        public CustomArray<T> Find(Func<T, bool> predicate);

        public bool Exists(Func<T, bool> predicate);
    }
}
