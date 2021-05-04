using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomHashSet;
using CustomCollectionsGeneric.Services.CustomList;
using CustomCollectionsGeneric.Services.CustomQueue;
using CustomCollectionsGeneric.Services.CustomStack;

namespace CustomCollectionsGeneric.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new CustomArray<int>(2);
            var list = new CustomList<int>();
            var queue = new CustomQueue<int>();
            var stack = new CustomStack<int>();
            var hashset = new CustomHashSet<int>();
        }
    }
}
