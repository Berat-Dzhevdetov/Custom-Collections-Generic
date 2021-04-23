using CustomCollectionsGeneric.Services.CustomHashSet;
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
            hashset = new CustomHashSet<int>() { 10,20,30,40,50 };
        }
        [TestMethod]
        public void AddShouldAddGivenItem()
        {
            //Asert
            var expectedResult = true;
            //Act
            var actualResult = hashset.Add(60);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
