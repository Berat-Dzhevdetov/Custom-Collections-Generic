using CustomCollectionsGeneric.Services.CustomArray;
using System;

namespace CustomCollectionsGeneric.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new CustomArray<int>(5);
            a[0] = 10;
            a[1] = 20;
            a[2] = 30;
            a[3] = 40;
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

            var b = new int[2];
           // b.
        }
    }
}
