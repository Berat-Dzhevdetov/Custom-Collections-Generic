using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollectionsGeneric.Services.CustomQueue
{
    public interface ICustomQueue<T>
    {
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        T[] ToArray();
    }
}
