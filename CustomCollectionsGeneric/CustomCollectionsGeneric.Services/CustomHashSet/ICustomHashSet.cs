using CustomCollectionsGeneric.Services.CustomArray;

namespace CustomCollectionsGeneric.Services.CustomHashSet
{
    public interface ICustomHashSet<T>
    {
        bool Add(T item);
        void Clear();
        bool Contains(T item);
        void CopyTo(out CustomArray<T> array);
        void CopyTo(out CustomArray<T> array,int startIndex);

    }
}
