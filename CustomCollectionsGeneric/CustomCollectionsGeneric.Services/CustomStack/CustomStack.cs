using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using System;
using System.Collections;
using System.Collections.Generic;
using static CustomCollectionsGeneric.Services.Message;

namespace CustomCollectionsGeneric.Services.CustomStack
{
    class CustomStack<T> : ICustomStack<T>, IEnumerable<T>
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

        public void Clear() => stack.Clear();

        public bool Contains(T item) => stack.Contains(item);

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException(connotGetElementFromStackWhenIsEmpty);

            return stack[Count-1];
        }

        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException(connotGetElementFromStackWhenIsEmpty);

            T elementToReturn = stack[Count - 1];

            stack.RemoveAt(Count - 1);

            return elementToReturn;
        }

        public void Push(T item)
        {
            stack.Insert(Count, item);
        }

        public CustomArray<T> ToArray() => stack.ToArray();
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
