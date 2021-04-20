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
            array[0] = 10;
            array[1] = 50;
            array[2] = 40;
            array[3] = 10;
            array[4] = 10;
            CustomList<int> list = new CustomList<int>(array);
            //bool a = list.Remove(10);
            //bool a1 = list.Remove(10);
            //bool a2 = list.Remove(40);
             list.RemoveAt(1);
            ;
        }
    }
}
