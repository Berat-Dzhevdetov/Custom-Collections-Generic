using CustomCollectionsGeneric.Services.CustomArray;

namespace CustomCollectionsGeneric.Services.CustomQueue
{
    public interface ICustomQueue<T>
    {
        void Clear();
        bool Contains(T item);
        void CopyTo(CustomArray<T> array, int arrayIndex);
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        CustomArray<T> ToArray();
    }
}
