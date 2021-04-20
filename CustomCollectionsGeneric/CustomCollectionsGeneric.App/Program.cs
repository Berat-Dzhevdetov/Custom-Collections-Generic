using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
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
            CustomList<int> list = new CustomList<int>(array);
            list.Remove(20);
            list.Remove(10);
            list.Remove(30);
            ;
        }
    }
}
