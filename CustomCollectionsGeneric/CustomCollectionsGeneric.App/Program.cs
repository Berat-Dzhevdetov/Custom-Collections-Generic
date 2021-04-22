using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using System;
using System.Collections.Generic;

namespace CustomCollectionsGeneric.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var array = new CustomArray<int>(4);
            array[0] = 10;
            array[1] = 20;
            array[2] = 40;
            array[3] = 50;
            CustomList<int> list = new CustomList<int>();
            var a = array.ToList();
            var b =a.GetType().Name;
            ;
            var list2 = new List<int>();
            var fonds = list.FindAll(x => x == 3);
        }
    }
}
