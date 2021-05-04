using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using System;
using System.Collections;
using System.Collections.Generic;
using static CustomCollectionsGeneric.Services.Message;

namespace CustomCollectionsGeneric.Services.CustomStack
{
    public class CustomStack<T> : ICustomStack<T>, IEnumerable<T>
    {
        private CustomList<T> stack;
        private int currentIndex;
        public int Count => stack.Count;
        public CustomStack()
        {
            this.stack = new CustomList<T>();
        }
        public CustomStack(CustomList<T> list)
            : this()
        {
            stack.AddRange(list);
        }

        /// <summary>
        /// All data in the stack will be cleared
        /// </summary>
        public void Clear() => stack.Clear();

        /// <summary>
        /// Checks if the given item is in the Stack
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns>True if item is in the Stack; otherwise false</returns>
        public bool Contains(T item) => stack.Contains(item);
        /// <summary>
        /// Looks the last item of the Stack
        /// </summary>
        /// <returns>Last item of the Stack</returns>
        ///  <exception cref="System.InvalidOperationException">Exception can be thrown if the Stack is empty</exception>
        public T Peek()
        {
            if (IsStackEmpty())
                throw new InvalidOperationException(connotGetElementFromStackWhenIsEmpty);

            return stack[Count - 1];
        }
        /// <summary>
        /// Looks the last item of the Stack
        /// </summary>
        /// <param name="item">Last item of the Stack</param>
        /// <returns>Returns true if there is a item; otherwise false</returns>
        public bool TryPeek(out T item)
        {
            item = default(T);

            if (IsStackEmpty())
                return false;

            item = stack[Count - 1];

            return true;
        }
        /// <summary>
        /// Removes the last item of the Stack
        /// </summary>
        /// <returns>Removed item</returns>
        ///  <exception cref="System.InvalidOperationException">Exception can be thrown if the Stack is empty</exception>
        public T Pop()
        {
            if (IsStackEmpty())
                throw new InvalidOperationException(connotGetElementFromStackWhenIsEmpty);

            T elementToReturn = stack[Count - 1];

            stack.RemoveAt(Count - 1);

            return elementToReturn;
        }
        /// <summary>
        /// Removes the last item of the Stack
        /// </summary>
        /// <param name="item">Last item of the Stack</param>
        /// <returns>Returns true if there is a item; otherwise false</returns>
        public bool TryPop(out T item)
        {
            item = default(T);

            if (IsStackEmpty())
                return false;

            item = stack[Count - 1];

            stack.RemoveAt(Count - 1);

            return true;
        }
        /// <summary>
        /// Adds given item to the Stack
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Push(T item)
        {
            stack.Add(item);
        }
        /// <summary>
        /// Make from stack to array
        /// </summary>
        /// <returns>New array from the stack</returns>
        public CustomArray<T> ToArray() => stack.ToArray();

        private bool IsStackEmpty()
        {
            if (Count <= 0)
                return true;

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            while (HasNext())
            {
                yield return stack[currentIndex];
                MoveNext();
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool HasNext() =>
             this.currentIndex < this.Count;

        public void Reset()
        {
            currentIndex = 0;
        }
        private bool MoveNext()
        {
            if (this.HasNext())
            {
                this.currentIndex++;
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)stack).GetEnumerator();
        }
    }
}
