﻿using System;
using System.Collections.Generic;
using static CustomCollectionsGeneric.Services.Message;

namespace CustomCollectionsGeneric.Services.CustomQueue
{
    public class CustomQueue<T> : ICustomQueue<T>
    {
        private List<T> queue;
        public int Count => queue.Count;


        public CustomQueue()
        {
            this.queue = new List<T>();
        }
        /// <summary>
        /// Invonking this method will clear all the data in your Queue
        /// </summary>
        public void Clear()
        {
            this.queue = new List<T>();
        }

        /// <summary>
        /// Checks if the given item is in the Queue
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns>True if item is in the Queue; otherwise false</returns>
        public bool Contains(T item) =>
             queue.Contains(item);

        /// <summary>
        /// Copies items from given index till the end and assign them to the given array.
        /// Your array should be empty or it can delete some of your data
        /// </summary>
        /// <param name="array">Array to set items in</param>
        /// <param name="arrayIndex">Start index from Queue</param>
        /// <exception cref="System.ArgumentException">Exception can be thrown if the given array is null or the given index is bigger than the queue's count</exception>
        /// <exception cref="System.IndexOutOfRangeException">Exception can be thrown if given index is less than zero</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentException(givenArrayWasNull);
            if (arrayIndex < 0)
                throw new IndexOutOfRangeException(givenParametarWasOutOfRange);
            if (arrayIndex >= queue.Count)
                throw new ArgumentException(theGivenParametarWasTooBig);

            int n = queue.Count - arrayIndex;
            int counter = 0;
            for (int i = n; i < queue.Count; i++, counter++)
            {
                array[counter] = queue[i];
            }
        }

        /// <summary>
        /// Adds given item to the Queue
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Enqueue(T item)
        {
            if (queue.Contains(item))
                return;
            queue.Add(item);
        }

        /// <summary>
        /// Removes the first item of the Queue
        /// </summary>
        /// <returns>Removed item</returns>
        /// <exception cref="System.IndexOutOfRangeException">Exception can be thrown if the Queue is empty</exception>
        public T Dequeue()
        {
            if (queue.Count <= 0)
                throw new InvalidOperationException(emptyQueue);
            var currentItem = queue[0];
            queue.RemoveAt(0);
            return currentItem;
        }

        /// <summary>
        /// Looks the first item of the Queue
        /// </summary>
        /// <returns>First item of the Queue</returns>
        /// <exception cref="System.IndexOutOfRangeException">Exception can be thrown if the Queue is empty</exception>
        public T Peek()
        {
            if (queue.Count <= 0)
                throw new InvalidOperationException(emptyQueue);
            return queue[0];
        }

        /// <summary>
        /// Make from queue to array
        /// </summary>
        /// <returns>New array of the queue</returns>
        public T[] ToArray() => queue.ToArray();


    }
}