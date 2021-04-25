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
            var queue = new CustomQueue<int>();
            //Act
            var array = queue.ToArray();
            //Assert
            var a =  array.GetType().Name;

            var c = new CustomList<int>() { 0, 2, 3, 4, 5 };
            c.Reverse();
            Stack<int> sa = new Stack<int>();
            sa.Peek();
        }
    }
}
