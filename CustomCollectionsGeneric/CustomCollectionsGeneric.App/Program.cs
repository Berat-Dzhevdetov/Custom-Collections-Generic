using CustomCollectionsGeneric.Services.CustomArray;
using System;

namespace CustomCollectionsGeneric.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new CustomArray<int>(5);
            array[0] = 10;
            array[1] = 20;
            array[2] = 30;
            array[3] = 40;
            array[4] = 50;
            array.Clear(3,4);
        }
    }
}
