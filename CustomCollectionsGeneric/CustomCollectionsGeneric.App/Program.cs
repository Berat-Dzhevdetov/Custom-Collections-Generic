using CustomCollectionsGeneric.Services.CustomArray;
using System;

namespace CustomCollectionsGeneric.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new CustomArray<int>(5);
            array[0] = 30;
            array[1] = 50;
            array[2] = 40;
            array[3] = 20;
            array[4] = 10;
            array.Sort();
        }
    }
}
