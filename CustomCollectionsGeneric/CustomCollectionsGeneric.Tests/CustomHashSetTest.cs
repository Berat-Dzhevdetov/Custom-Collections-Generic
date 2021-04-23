using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomHashSet;
using CustomCollectionsGeneric.Services.CustomList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomCollectionsGeneric.Tests
{
    [TestClass]
    public class CustomHashSetTest
    {
        CustomHashSet<int> hashset;
        [TestInitialize]
        public void SetUp()
        {
            hashset = new CustomHashSet<int>() { 10, 20, 30, 40, 50, 60 };
        }
        [TestMethod]
        public void AddShouldAddGivenItemAndReturnTrue()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var actualResult = hashset.Add(600);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AddShouldNotAddGivenItemAndReturnFalse()
        {
            //Arrange
            var expectedResult = false;
            //Act
            var actualResult = hashset.Add(10);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AddRangeShouldAddOnlyValuesThatAreNotInTheHashSetAlready()
        {
            //Arrange
            var customList = new CustomList<int>() { 100, 43, 40, 10, 10, 60, 5 };
            //Act
            hashset.AddRange(customList);
            int[] expectedResult = new[] { 10, 20, 30, 40, 50, 60, 100, 43, 5 };
            //Assert
            for (int i = 0; i < hashset.Count; i++)
            {
                Assert.AreEqual(true, hashset.Remove(expectedResult[i]));
                Assert.AreEqual(false, hashset.Remove(expectedResult[i]));
            }
        }
        [TestMethod]
        public void AnyShouldReturnTrueIfThereIsAElementInTheHashSet()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var actualResult = hashset.Any();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AnyShouldReturnFalseIfThereIsNoElementInTheHashSet()
        {
            //Arrange
            var expectedResult = false;
            hashset = new CustomHashSet<int>();
            //Act
            var actualResult = hashset.Any();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AnyWithPredicateShouldFindTen()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var actualResult = hashset.Any(x => x == 10);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void AnyWithPredicateShouldnotFindAHundred()
        {
            //Arrange
            var expectedResult = false;
            //Act
            var actualResult = hashset.Any(x => x == 100);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ClearShouldClearTheWholeHashSet()
        {
            //Arrange
            var expectedResult = 0;
            //Act
            hashset.Clear();
            var actualResult = hashset.Count;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ContainsShouldReturnTrueIfWeAreLookingForTen()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var actualResult = hashset.Contains(10);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void ContainsShouldReturnFalseIfWeAreLookingForAThousand()
        {
            //Arrange
            var expectedResult = false;
            //Act
            var actualResult = hashset.Contains(1000);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void CopyToShouldPutsTheValuesIntoGivenArray()
        {
            //Arrange
            var expectedResult = hashset.Count;
            var tempArray = new CustomArray<int>(1);
            //Act
            hashset.CopyTo(out tempArray);
            var actualResult = tempArray.Length;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void CopyToWithStartIndexShouldCopyOnlyHalfOfTheHashSet()
        {
            //Arrange
            var expectedResult = 3;
            var tempArray = new CustomArray<int>(1);
            //Act
            hashset.CopyTo(out tempArray, 3);
            var actualResult = tempArray.Length;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void IndexOfShouldReturnZeroBasedIndexOfTen()
        {
            //Arrange
            var expectedResult = 0;
            //Act
            var actualResult = hashset.IndexOf(10);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void IndexOfShouldReturnMinusOneBecauseItemIsNotFound()
        {
            //Arrange
            var expectedResult = -1;
            //Act
            var actualResult = hashset.IndexOf(100);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void InsertShouldInsertSeventyInTheEndOfTheHashSet()
        {
            //Arrange
            var expectedResult = true;
            //Act
            var actualResult = hashset.Insert(hashset.Count - 1, 70);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void RemoveShouldRemoveItemAndReturnTrue()
        {
            //Arrange
            var expectedCount = hashset.Count - 1;
            var expectedBool = true;
            //Act
            var actualBool = hashset.Remove(60);
            var actualCount = hashset.Count;
            //Assert
            Assert.AreEqual(expectedBool, actualBool);
            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void RemoveShouldnotRemoveItemAndReturnFalse()
        {
            //Arrange
            var expectedCount = hashset.Count;
            var expectedBool = false;
            //Act
            var actualBool = hashset.Remove(600);
            var actualCount = hashset.Count;
            //Assert
            Assert.AreEqual(expectedBool, actualBool);
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
