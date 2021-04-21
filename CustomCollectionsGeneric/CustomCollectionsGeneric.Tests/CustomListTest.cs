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
    }
}
