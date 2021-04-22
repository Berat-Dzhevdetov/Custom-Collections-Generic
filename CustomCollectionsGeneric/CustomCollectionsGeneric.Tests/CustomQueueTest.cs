using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomCollectionsGeneric.Services.CustomQueue;
using System;
using static CustomCollectionsGeneric.Services.Message;
using CustomCollectionsGeneric.Services.CustomArray;

namespace CustomCollectionsGeneric.Tests
{
    [TestClass]
    public class CustomQueueTest
    {
        [TestMethod]
        public void ShouldCannotAddSameValues()
        {
            //Arange
            var queue = new CustomQueue<int>();
            int exceptedResult = 1;
            //Act
            queue.Enqueue(1);
            queue.Enqueue(1);
            //Assert
            Assert.AreEqual(exceptedResult, queue.Count);
        }
        [TestMethod]
        public void ShouldAddIntOneAndThenGetIt()
        {
            //Arange
            var queue = new CustomQueue<int>();
            int exceptedResult = 1;
            //Act
            queue.Enqueue(1);
            int number = queue.Dequeue();
            //Assert
            Assert.AreEqual(exceptedResult, number);
        }
        [TestMethod]
        public void ShouldAddStringAndThenGetIt()
        {
            //Arange
            var queue = new CustomQueue<string>();
            string exceptedResult = "Hello";
            //Act
            queue.Enqueue("Hello");
            string word = queue.Dequeue();
            //Assert
            Assert.AreEqual(exceptedResult, word);
        }
        [TestMethod]
        public void TryingToDequeueBeforePutAnythingInQueueShouldThrowInvalidOperationException()
        {
            //Arange
            var queue = new CustomQueue<string>();
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
            //Assert
            Assert.AreEqual(emptyQueue, ex.Message);
        }

        [TestMethod]
        public void CopyToShouldThrowArgumentExceptionIfGivenArrayIsNull()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            queue.Enqueue(1);
            var ex = Assert.ThrowsException<ArgumentException>(() => queue.CopyTo(null, 0));
            //Assert
            Assert.AreEqual(givenArrayWasNull, ex.Message);
        }
        [TestMethod]
        public void CopyToShouldThrowIndexOutOfRangeExceptionIfGivenIndexIsLessThanZero()
        {
            //Arange
            var queue = new CustomQueue<int>();
            var array = new CustomArray<int>(queue.Count);
            //Act
            queue.Enqueue(1);
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => queue.CopyTo(array, -1));
            //Assert
            Assert.AreEqual(givenParametarWasOutOfRange, ex.Message);
        }
        [TestMethod]
        public void CopyToShouldThrowArgumentExceptionIfGivenIndexIsTooBig()
        {
            //Arange
            var queue = new CustomQueue<int>();
            var array = new CustomArray<int>(queue.Count);
            //Act
            queue.Enqueue(1);
            var ex = Assert.ThrowsException<ArgumentException>(() => queue.CopyTo(array, 2));
            //Assert
            Assert.AreEqual(theGivenParametarWasTooBig, ex.Message);
        }

        [TestMethod]
        public void ClearShouldClearAllData()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Clear();
            //Assert
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void ContainsShouldReturnTrueIfWeAddOneAndThenLookForIt()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            queue.Enqueue(1);
            //Assert
            Assert.AreEqual(true, queue.Contains(1));
        }
        [TestMethod]
        public void ContainsShouldReturnFalseIfItemWeAreLookingForIsNotInQueue()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            queue.Enqueue(1);
            //Assert
            Assert.AreEqual(false, queue.Contains(2));
        }
        [TestMethod]
        public void PeekShouldThrowInvalidOperationExceptionIfThereIsNoItemInQueue()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() => queue.Peek());
            //Assert
            Assert.AreEqual(emptyQueue, ex.Message);
        }
        [TestMethod]
        public void PeekShouldReturnFirstItemOfTheQueue()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(5);
            queue.Enqueue(4);
            queue.Enqueue(3);
            //Assert
            Assert.AreEqual(1, queue.Peek());
        }
        [TestMethod]
        public void ToArrayShouldMakeQueueToArrayNoItems()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            var array = queue.ToArray();
            //Assert
            Assert.AreEqual("CustomArray`1", array.GetType().Name);
        }
        [TestMethod]
        public void ToArrayShouldMakeQueueToArrayWithItems()
        {
            //Arange
            var queue = new CustomQueue<int>();
            //Act
            queue.Enqueue(2);
            queue.Enqueue(1);
            queue.Enqueue(3);
            queue.Enqueue(4);
            var array = queue.ToArray();
            //Assert
            Assert.AreEqual(2, array[0]);
            Assert.AreEqual(1, array[1]);
            Assert.AreEqual(3, array[2]);
            Assert.AreEqual(4, array[3]);
        }
        [TestMethod]
        public void GetEnumerator()
        {
            //Arange
            var queue = new CustomQueue<int>();
            var expectedResult = new int[] { 2,1,3,4 };
            int counter = 0;
            //Act
            queue.Enqueue(2);
            queue.Enqueue(1);
            queue.Enqueue(3);
            queue.Enqueue(4);
            //Assert
            foreach (var item in queue)
            {
                Assert.AreEqual(expectedResult[counter], item);
                counter++;
            }
        }
        [TestMethod]
        public void TryPeekShouldReturnDefaultValueBecauseNoItems()
        {
            //Arange
            var queue = new CustomQueue<string>();
            string result;
            //Act
            var act = queue.TryPeek(out result);
            //Assert
            Assert.AreEqual(null, result);
            Assert.AreEqual(false, act);
        }
        [TestMethod]
        public void TryPeekShouldReturnFirstItem()
        {
            //Arange
            var queue = new CustomQueue<string>();
            string result;
            queue.Enqueue("1");
            queue.Enqueue("3");
            queue.Enqueue("4");
            //Act
            var act = queue.TryPeek(out result);
            //Assert
            Assert.AreEqual("1", result);
            Assert.AreEqual(true, act);
        }
        [TestMethod]
        public void TryDequeueShouldReturnDefaultValueBecauseNoItems()
        {
            //Arange
            var queue = new CustomQueue<string>();
            string result;
            //Act
            var act = queue.TryDequeue(out result);
            //Assert
            Assert.AreEqual(null, result);
            Assert.AreEqual(false, act);
        }
        [TestMethod]
        public void TryDequeueShouldReturnFirstItem()
        {
            //Arange
            var queue = new CustomQueue<string>();
            string result;
            queue.Enqueue("1");
            queue.Enqueue("3");
            queue.Enqueue("4");
            //Act
            var act = queue.TryDequeue(out result);
            //Assert
            Assert.AreEqual("1", result);
            Assert.AreEqual(true, act);
        }
    }
}
