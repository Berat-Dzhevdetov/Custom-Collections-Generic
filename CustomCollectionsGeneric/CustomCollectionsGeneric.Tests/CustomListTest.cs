using CustomCollectionsGeneric.Services.CustomArray;
using CustomCollectionsGeneric.Services.CustomList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CustomCollectionsGeneric.Tests
{
    [TestClass]
    public class CustomListTest
    {
        CustomList<int> list;
        [TestInitialize]
        public void Initialize()
        {
            list = new CustomList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
        }
        [TestMethod]
        public void ConstructorShouldInitializeTheArrayWithDeafaultCapacity()
        {
            var cutomList = new CustomList<int>();
            var expextedResult = 2;
            var actualResult = cutomList.Capacity;
            Assert.AreEqual(expextedResult, actualResult);
        }

        [TestMethod]
        public void ConstructorRecivesArrayAndInitializeTheArrayAndSetValuesFromTheInput()
        {
            var customArr = new CustomArray<int>(2);
            customArr[0] = 1;
            customArr[1] = 2;
            var cutomList = new CustomList<int>(customArr);
            var expextedResult = 2;
            Assert.AreEqual(expextedResult, cutomList.Count);
        }
        [TestMethod]
        public void ConstructorRecivesListAndInitializeTheArrayAndSetValuesFromTheInput()
        {
            var customArr = new CustomList<int>();
            customArr.Add(1);
            customArr.Add(2);
            var cutomList = new CustomList<int>(customArr);
            var expextedResult = 2;
            Assert.AreEqual(expextedResult, cutomList.Count);
        }
        [TestMethod]
        public void IfCustomListIsReadOnlyAndReciveElementShouldThrowExeption()
        {
            list.IsReadOnly(true);
            Assert.ThrowsException<FieldAccessException>(() => list.Add(5));
        }
        [TestMethod]
        public void CustomListAddMethodShouldAddTheElement()
        {
            var expextedResult = list.Count + 1;
            list.Add(2);
            Assert.AreEqual(expextedResult, list.Count);
        }

        [TestMethod]
        public void CustomListAddRangeMethodShouldAddTheElementsWhenGivenList()
        {
            var testList = new CustomList<int>();
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);
            var expextedResult = list.Count + testList.Count;
            list.AddRange(testList);
            Assert.AreEqual(expextedResult, list.Count);
        }
        [TestMethod]
        public void CustomListAddRangeMethodShouldAddTheElementsWhenGivenArray()
        {
            var testArr = new CustomArray<int>(5);
            testArr[0]=1;
            testArr[1]=1;
            testArr[2]=1;
            testArr[3]=1;
            testArr[4]=1;
            var expextedResult = list.Count + testArr.Length;
            list.AddRange(testArr);
            Assert.AreEqual(expextedResult, list.Count);
        }

        [TestMethod]
        public void ClearMethodShouldRemoveAllElements()
        {
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }
        [TestMethod]
        [DataRow(true,3)]
        [DataRow(false, 213213)]
        public void ContainsMethodShouldFindElementsAndReturnTrueIfItsFind(bool expResult,int toBeFind)
        {
            Assert.AreEqual(expResult, list.Contains(toBeFind));
        }
        [TestMethod]
        [DataRow(true, 4)]
        [DataRow(false, 3434)]
        //work with anonymous function
        public void ExistMethodShouldFindElementsAndReturnTrueIfItsFind(bool expResult, int toBeFind)
        {
            Assert.AreEqual(expResult, list.Exists(x=>x==toBeFind));
        }

        [TestMethod]
        [DataRow(4, 4)]
        [DataRow(0, 3434)]
        public void FindMethodShouldReturnTheValueThatFoundAndTheDeafaultValueIfNothinfFound(int expResult, int toBeFind)
        {
            Assert.AreEqual(expResult, list.Find(x => x == toBeFind));
        }

        [TestMethod]
        public void FindAllShouldReturnAllMatchedThingWhitTheGivenConditionAndIfNothingIsFoundReturnEmptyCollection()
        {
            list.Add(1);
            var expextedResult = 2;
            var actualResult = list.FindAll(x => x == 1);
            Assert.AreEqual(expextedResult, actualResult.Count);

            var expextedResultIfNotFoundAnything = 0;
            actualResult = list.FindAll(x => x == 342);
            Assert.AreEqual(expextedResultIfNotFoundAnything, actualResult.Count);
        }

        [TestMethod]
        [DataRow(4, 5)]
        [DataRow(-1, 3434)]
        public void IndexOfMethodShoulReturnTheIndexOfGivenItemOrReturnMinusOneIfNotFound(int expResult,int searchedItem)
        {
            Assert.AreEqual(expResult, list.IndexOf(searchedItem));
        }

        [TestMethod]
        [DataRow(34, 1)]
        [DataRow(54, 4)]
        [DataRow(5, 3)]
        [DataRow(12, 2)]
        public void InsertMethodShouldInsertItemAtTheGivenIndex(int insertedItem, int index)
        {
            list.Insert( index, insertedItem);
            Assert.AreEqual(insertedItem, list[index]);
        }

        [TestMethod]
        public void InsertMethodShouldThrowExeptionIfInvalidIndexOrIsReadOnlyCollection()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => list.Insert(2134213,4));

            list.IsReadOnly(true);
            Assert.ThrowsException<FieldAccessException>(() => list.Add(5));
        }

        [TestMethod]
        [DataRow(4,3)]
        [DataRow(234,-1)]
        public void LastIndexOfMethodReturnsTheFirstMatchedItemIndexWhitTheGivenItemAndIfNotFoundReturnMinusOne(int item,int expextedResult)
        {
            var actualResult = list.LastIndexOf(item);
            Assert.AreEqual(expextedResult, actualResult);
        }

        [TestMethod]
        [DataRow(1, true)]
        [DataRow(3, true)]
        [DataRow(2, true)]
        [DataRow(4, true)]
        [DataRow(5, true)]
        [DataRow(45, false)]
        public void RemoveMethodShouldRemoveFirstItemThatMatchTheItem(int toRemove,bool expResult)
        {
            bool actualResult = list.Remove(toRemove);
            Assert.AreEqual(expResult, actualResult);
        }
        [TestMethod]
        [DataRow(4, true)]
        [DataRow(5, true)]
        [DataRow(45, false)]
        public void RemoveAllMethodShouldRemoveAllItemThatMatchTheItem(int toRemove, bool expResult)
        {
            bool actualResult = list.RemoveAll(toRemove);
            Assert.AreEqual(expResult, actualResult);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(4)]
        [DataRow(2)]
        public void RemoveAtMethodShouldRemoveItemAtGivenIndex(int index)
        {
            var expResult = list.Count - 1;
            list.RemoveAt(index);
            Assert.AreEqual(expResult, list.Count);

        }
        [TestMethod]
        [DataRow(-23)]
        [DataRow(54)]
        [DataRow(895687)]
        public void RemoveAtMethodShouldThrowExeptionWhenGivenInvalidIndex(int index)
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => list.RemoveAt(index));
        }

        [TestMethod]
        public void ReverseMethodShoulRevurseTheList()
        {
            var testList =new CustomList<int>();
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);

            testList.Reverse();

            var listCounter = 0;
            for (int i = list.Count-1; i >= 0; i--)
            {
                Assert.AreEqual(list[listCounter++], testList[i]);
            }
        }

        //Sort,SortDescending Tests will be done soon due to bugs.
        [TestMethod]
        public void SortMethodShoulSortTheArray()
        {
            var testList = new CustomList<int>();
            testList.Add(3);
            testList.Add(2);
            testList.Add(5);
            testList.Add(1);
            testList.Add(4);
            testList.Sort();

            for (int i = 0 ; i <list.Count; i++)
            {
                Assert.AreEqual(list[i], testList[i]);
            }
        }
        [TestMethod]
        public void SortDescendingMethodShoulSortTheArray()
        {
            var testList = new CustomList<int>();
            testList.Add(3);
            testList.Add(2);
            testList.Add(5);
            testList.Add(1);
            testList.Add(4);

            list.Reverse();

            testList.SortDescending();

            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(list[i], testList[i]);
            }
        }

        [TestMethod]
       public void AnyMethodShoulFindAtLeastOneMatch()
        {
            Assert.AreEqual(true, list.Any(x=>x==1));
        }
    }
}
