using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using CustomCollectionsGeneric.Services.CustomQueue;
using System;
using System.Collections.Generic;

namespace CustomCollectionsGeneric.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var a = new CustomArray<int>(1);
            b.CopyTo(out a);
            ;
        }
    }
}
