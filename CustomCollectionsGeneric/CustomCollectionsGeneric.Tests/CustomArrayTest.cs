using CustomCollectionsGeneric.Services.CustomArray;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CustomCollectionsGeneric.Services.Message;
using System;
using System.Text;
using CustomCollectionsGeneric.Services.CustomList;

namespace CustomCollectionsGeneric.Tests
{
    [TestClass]
    public class CustomArrayTest
    {
        CustomArray<int> array;
        [TestInitialize]
        public void Initialize()
        {
            array = new CustomArray<int>(5);
            array[0] = 10;
            array[1] = 20;
            array[2] = 30;
            array[3] = 40;
            array[4] = 50;
        }
        [TestMethod]
        public void CreatingNewInstanceWithLengthFive()
        {
            int exceptedResult = 5;
            //Assert
            Assert.AreEqual(exceptedResult, array.Length);
        }
        [TestMethod]
        public void TryingToAccessIndexOfTheArray()
        {
            //Arange
            int exceptedResult = 30;
            //Assert
            Assert.AreEqual(exceptedResult, array[2]);
        }
        [TestMethod]
        public void TryingToAccessIndexOfTheArrayBiggerThanTheLength()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array[64]);
            //Assert
            Assert.AreEqual(ex.Message, theGivenParametarWasTooBig);
        }
        [TestMethod]
        public void TryingToAccessIndexOfTheArrayLessThanZero()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array[-1]);
            //Assert
            Assert.AreEqual(ex.Message, lessThanZero);
        }
        [TestMethod]
        public void TryingToChangeValueInIndexOfTheArrayThatIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array[2] = 42);
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void AsReadOnlyShouldReturnReadOnlyCollection()
        {
            //Arange
            var a = array.AsReadOnly();
            //Act
            var exceptedResult = 20;
            //Assert
            Assert.AreEqual(exceptedResult, a[1]);
        }
        [TestMethod]
        public void ClearShouldThrowIndexOutOfRangeExceptionWithBigStartIndex()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array.Clear(78, 3));
            //Assert
            Assert.AreEqual(ex.Message, theGivenParametarWasTooBig);
        }
        [TestMethod]
        public void ClearShouldThrowIndexOutOfRangeExceptionWithBigLength()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array.Clear(0, 45));
            //Assert
            Assert.AreEqual(ex.Message, theGivenParametarWasTooBig);
        }

        [TestMethod]
        public void ClearShouldThrowIndexOutOfRangeExceptionWithLessThanZeroStartIndex()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array.Clear(-78, 3));
            //Assert
            Assert.AreEqual(ex.Message, lessThanZero);
        }
        [TestMethod]
        public void ClearShouldThrowIndexOutOfRangeExceptionWithLessThanZeroLength()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array.Clear(0, -1));
            //Assert
            Assert.AreEqual(ex.Message, lessThanZero);
        }
        [TestMethod]
        public void ClearShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arrange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Clear(0, 3));
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void ClearShouldClearEvenIfIndexIsOneOfLastsAndLengthIsTooBig()
        {
            //Act
            array.Clear(3, 4);
            //Assert
            var expectedResuls = new[] { 10, 20, 30, 0, 0 };
            for (int i = 0; i < expectedResuls.Length; i++)
            {
                Assert.AreEqual(expectedResuls[i], array[i]);
            }
        }
        [TestMethod]
        public void ClearWithOneParameterShouldClearFromGivenIndexTillTheEnd()
        {
            //Act
            array.Clear(2);
            //Assert
            var expectedResuls = new[] { 10, 20, 0, 0, 0 };
            for (int i = 0; i < expectedResuls.Length; i++)
            {
                Assert.AreEqual(expectedResuls[i], array[i]);
            }
        }
        [TestMethod]
        public void ClearWithOneParameterShouldThrowIndexOutOfRangeExceptionWithTooBigGivenIndex()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array.Clear(110));
            //Assert
            Assert.AreEqual(ex.Message, theGivenParametarWasTooBig);
        }
        [TestMethod]
        public void ClearWithOneParameterShouldThrowIndexOutOfRangeExceptionWithIndexLessThanZero()
        {
            //Act
            var ex = Assert.ThrowsException<IndexOutOfRangeException>(() => array.Clear(-110));
            //Assert
            Assert.AreEqual(ex.Message, lessThanZero);
        }
        [TestMethod]
        public void ClearWithOneParameterShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Clear(1));
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void ClearWithNoParametersShouldMakeNewlyArray()
        {
            //Act
            array.Clear();
            //Assert
            var expectedResuls = new[] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < expectedResuls.Length; i++)
            {
                Assert.AreEqual(expectedResuls[i], array[i]);
            }
        }
        [TestMethod]
        public void ClearWithNoParametersShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Clear());

            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void CloneShouldReturnNewlyClonedCustomArray()
        {
            var expectedResuls = new[] { 10, 20, 30, 40, 50 };
            //Act
            var newArr = array.Clone();

            //Assert
            for (int i = 0; i < expectedResuls.Length; i++)
            {
                Assert.AreEqual(expectedResuls[i], newArr[i]);
            }
            newArr[0] = 9;
            Assert.AreNotEqual(array[0], newArr[1]);
        }
        [TestMethod]
        public void GetEnumerator()
        {
            //Arrange
            var expectedResuls = "10\r\n20\r\n30\r\n40\r\n50\r\n";
            //Act
            var sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.AppendLine(item.ToString());
            }
            //Assert
            Assert.AreNotEqual(expectedResuls, sb.ToString().TrimEnd());
        }
        [TestMethod]
        public void EmptyShouldReturnNewlyEmptyArrayWithGivenLength()
        {
            var newlyArray = array.Empty(10);
            //Act
            var expectedLength = 10;
            var firstExpectedItem = 33;
            newlyArray[1] = 33;
            //Assert
            Assert.AreEqual(expectedLength, newlyArray.Length);
            Assert.AreEqual(firstExpectedItem, newlyArray[1]);
        }
        [TestMethod]
        public void EmptyShouldThrowArgumentExceptionIfGivenLengthIsLessThanZero()
        {
            //Act
            var ex = Assert.ThrowsException<ArgumentException>(() => array.Empty(0));
            //Assert
            Assert.AreEqual(ex.Message, cannotCreateEmptyArray);
        }
        [TestMethod]
        public void FillShouldFillTheWholeArrayWithGivenValue()
        {
            //Arrange
            var expectedResult = new[] { 100, 100, 100, 100, 100 };
            //Act
            array.Fill(100);
            //Assert
            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.AreEqual(expectedResult[i], array[i]);
            }
        }
        [TestMethod]
        public void FillShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arrange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Fill(100));
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void FindAllShouldFindInArrayWithPredicate()
        {
            //Arrange
            var expectedLength = 1;
            var expectedNumber = 20;
            //Act
            var newlyArray = array.FindAll(x => x == 20);
            //Assert
            Assert.AreEqual(expectedLength, newlyArray.Length);
            Assert.AreEqual(expectedNumber, newlyArray[0]);
        }
        [TestMethod]
        public void FindAllShouldNotFindAnyThingInArrayWithPredicate()
        {
            //Arrange
            var expectedLength = 0;
            //Act
            var newlyArray = array.FindAll(x => x >= 300);
            //Assert
            Assert.AreEqual(expectedLength, newlyArray.Length);

        }
        [TestMethod]
        public void FindShouldNotFindAnyThingInArrayWithPredicate()
        {
            //Arrange
            var expectedLength = 0;
            //Act
            var item = array.Find(x => x >= 300);
            //Assert
            Assert.AreEqual(expectedLength, item);

        }
        [TestMethod]
        public void FindShouldFindInArrayWithPredicate()
        {
            //Arrange
            var expectedResult = 30;
            //Act
            var item = array.Find(x => x == 30);
            //Assert
            Assert.AreEqual(expectedResult, item);

        }
        [TestMethod]
        public void ExistsShouldFindThatThereIsNumberBiggerThan40()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var newlyArray = array.Exists(x => x >= 40);
            //Assert
            Assert.AreEqual(expectedResult, newlyArray);
        }
        [TestMethod]
        public void ExistsShouldNotFindAnyThignkThatThereIsNumberBiggerThan400()
        {
            //Arrange
            var expectedResult = false;
            //Act
            var newlyArray = array.Exists(x => x >= 400);
            //Assert
            Assert.AreEqual(expectedResult, newlyArray);
        }
        [TestMethod]
        public void FindLastShouldFindLast40()
        {
            //Arrange
            array[2] = 40;
            var expectedResult = 40;
            //Act
            var item = array.FindLast(x => x == 40);
            //Assert
            Assert.AreEqual(expectedResult, item);
        }
        [TestMethod]
        public void IndexOfShouldReturnOneIfWeAreLookingFor20()
        {
            //Arrange
            var expectedResult = 1;
            //Act
            var item = array.IndexOf(20);
            //Assert
            Assert.AreEqual(expectedResult, item);
        }
        [TestMethod]
        public void IndexOfShouldReturnMinusOneIfWeAreLookingFor60()
        {
            //Arrange
            var expectedResult = -1;
            //Act
            var item = array.IndexOf(60);
            //Assert
            Assert.AreEqual(expectedResult, item);
        }
        [TestMethod]
        public void LastIndexOfShouldReturnOneIfWeAreLookingFor20()
        {
            //Arrange
            array[0] = 20;
            var expectedResult = 1;
            //Act
            var item = array.LastIndexOf(20);
            //Assert
            Assert.AreEqual(expectedResult, item);
        }
        [TestMethod]
        public void LastIndexOfShouldReturnMinusOneIfWeAreLookingFor60()
        {
            //Arrange
            var expectedResult = -1;
            //Act
            var item = array.LastIndexOf(60);
            //Assert
            Assert.AreEqual(expectedResult, item);
        }
        [TestMethod]
        public void ResizeShouldMakeArrayBigger()
        {
            //Arrange
            var expectedResult = array.Length + 3;
            //Act
            array.Resize(8);
            //Assert
            Assert.AreEqual(expectedResult, array.Length);
            Assert.AreEqual(10, array[0]);
            Assert.AreEqual(20, array[1]);
        }
        [TestMethod]
        public void ResizeShouldMakeArraySmaller()
        {
            //Arrange
            var expectedResult = array.Length - 3;
            //Act
            array.Resize(2);
            //Assert
            Assert.AreEqual(expectedResult, array.Length);
            Assert.AreEqual(10, array[0]);
            Assert.AreEqual(20, array[1]);
        }
        [TestMethod]
        public void ResizeShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Resize(12));
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);

        }
        [TestMethod]
        public void ReverseShouldReverseTheArray()
        {
            //Act
            array.Reverse();
            //Assert
            Assert.AreEqual(array[0], 50);
            Assert.AreEqual(array[1], 40);
            Assert.AreEqual(array[2], 30);
            Assert.AreEqual(array[3], 20);
            Assert.AreEqual(array[4], 10);
        }
        [TestMethod]
        public void ReverseShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Reverse());
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);

        }
        [TestMethod]
        public void SortShouldSortAcendingTheArray()
        {
            //Arange
            array[0] = 30;
            array[1] = 50;
            array[2] = 40;
            array[3] = 20;
            array[4] = 10;
            //Act
            array.Sort();
            //Assert
            Assert.AreEqual(array[0], 10);
            Assert.AreEqual(array[1], 20);
            Assert.AreEqual(array[2], 30);
            Assert.AreEqual(array[3], 40);
            Assert.AreEqual(array[4], 50);
        }
        [TestMethod]
        public void SortShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.Sort());
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void SortDescendingShouldSortAcendingTheArray()
        {
            //Arange
            array[0] = 30;
            array[1] = 50;
            array[2] = 40;
            array[3] = 20;
            array[4] = 10;
            //Act
            array.SortDescending();
            //Assert
            Assert.AreEqual(array[0], 50);
            Assert.AreEqual(array[1], 40);
            Assert.AreEqual(array[2], 30);
            Assert.AreEqual(array[3], 20);
            Assert.AreEqual(array[4], 10);
        }
        [TestMethod]
        public void SortDescendingShouldThrowFieldAccessExceptionIfArrayIsReadOnly()
        {
            //Arange
            array.IsReadOnly(true);
            //Act
            var ex = Assert.ThrowsException<FieldAccessException>(() => array.SortDescending());
            //Assert
            Assert.AreEqual(ex.Message, cannotAccessWhileArrayIsReadOnly);
        }
        [TestMethod]
        public void AnyShouldReturnTrueIfThereIsSomethingInTheArray()
        {
            //Arange
            var expectedResult = true;
            //Act
            var actualResult = array.Any();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AnyShouldReturnTrueIfThereIsSomethingInTheArrayThatMeetTheGivenConditions()
        {
            //Arange
            var expectedResult = true;
            //Act
            var actualResult = array.Any(x => x == 50);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AnyShouldReturnFalseIfThereIsSomethingInTheArrayThatMeetTheGivenConditions()
        {
            //Arange
            var expectedResult = false;
            //Act
            var actualResult = array.Any(x => x == 580);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ToListShouldReturnListWithValues()
        {
            //Arange
            var expectedType = "CustomList`1";
            var expectedValue = 10;
            var expectedCount = 5;
            //Act
            var actualType = array.ToList().GetType().Name;
            var list = array.ToList();
            //Assert
            Assert.AreEqual(expectedType, actualType);
            Assert.AreEqual(expectedValue, array[0]);
            Assert.AreEqual(expectedCount, list.Count);
        }
    }
}