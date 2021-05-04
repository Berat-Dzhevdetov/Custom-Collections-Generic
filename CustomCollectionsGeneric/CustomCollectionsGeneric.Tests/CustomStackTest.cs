using CustomCollectionsGeneric.Services.CustomStack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static CustomCollectionsGeneric.Services.Message;
using CustomCollectionsGeneric.Services.CustomArray;
namespace CustomCollectionsGeneric.Tests
{
    [TestClass]
    public class CustomStackTest
    {
        CustomStack<int> stack;
        [TestInitialize]
        public void SetUp()
        {
            stack = new CustomStack<int>();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);
            stack.Push(50);
            stack.Push(60);
        }
        [TestMethod]
        public void ClearShouldRemoveAllOfTheDataInTheStack()
        {
            //Arrange
            var expectedResult = 0;
            //Act
            stack.Clear();
            var actualResult = stack.Count;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ContainsShouldReturnTrueIfWeAreLookingFor20()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var actualResult = stack.Contains(20);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ContainsShouldReturnFalseIfWeAreLookingFor200()
        {
            //Arrange
            var expectedResult = false;
            //Act
            stack.Clear();
            var actualResult = stack.Contains(200);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void PeekShouldReturnLastElementFromTheStack()
        {
            //Arrange
            var expectedResult = 60;
            //Act
            var actualResult = stack.Peek();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void PeekShouldThrowInvalidOperationExceptionIfThereIsNoElementsInside()
        {
            //Act
            stack.Clear();
            var ex = Assert.ThrowsException<InvalidOperationException>(() => stack.Peek());
            //Assert
            Assert.AreEqual(connotGetElementFromStackWhenIsEmpty, ex.Message);
        }
        [TestMethod]
        public void TryPeekShouldReturnTrueAndGiveValueToGivenVariable()
        {
            //Arrange
            var expectedBool = true;
            var expectedNumber = 60;
            int actualNumber;
            //Act
            var actualBool = stack.TryPeek(out actualNumber);
            //Assert
            Assert.AreEqual(expectedBool, actualBool);
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        [TestMethod]
        public void TryPeekShouldReturnFalse()
        {
            //Arrange
            stack.Clear();
            var expectedBool = false;
            var expectedNumber = 0;
            int actualNumber;
            //Act
            var actualBool = stack.TryPeek(out actualNumber);
            //Assert
            Assert.AreEqual(expectedBool, actualBool);
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        [TestMethod]
        public void PopShouldReturn60AndDecrementTheCounter()
        {
            //Arrange
            var expectedResult = 60;
            var expectedCounter = stack.Count - 1;
            //Act
            var actualResult = stack.Pop();
            var actualCounter = stack.Count;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedCounter, actualCounter);
        }
        [TestMethod]
        public void PopShouldThrowInvalidOperationExceptionIfTheStackIsEmpty()
        {
            //Arrange
            stack.Clear();
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() => stack.Pop());
            //Assert
            Assert.AreEqual(connotGetElementFromStackWhenIsEmpty, ex.Message);
        }
        [TestMethod]
        public void TryPopShouldReturnTrueAndGiveValueToGivenVariable()
        {
            //Arrange
            var expectedBool = true;
            int actualNumber;
            var expectedNumber = 60;
            //Act
            var actualBool = stack.TryPop(out actualNumber);
            //Assert
            Assert.AreEqual(expectedBool, actualBool);
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        [TestMethod]
        public void PushShouldAddItemToTheEndOfTheStack()
        {
            //Arrange
            var expectedResult = 70;
            stack.Push(70);
            //Act
            var actualResult = stack.Pop();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ToArrayShouldReturnCustomArrayWithValuesCopied()
        {
            //Arrange
            var expectedResult = new CustomArray<int>(6);
            //Act
            var actualResult = stack.ToArray();
            //Assert
            Assert.AreEqual(expectedResult.GetType().Name,actualResult.GetType().Name);
            Assert.AreEqual(expectedResult.Length, actualResult.Length);
        }
    }
}
