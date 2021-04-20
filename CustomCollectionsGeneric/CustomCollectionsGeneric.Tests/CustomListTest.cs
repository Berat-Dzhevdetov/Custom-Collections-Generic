using CustomCollectionsGeneric.Services.CustomList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomCollectionsGeneric.Tests
{
    [TestClass]
    public class CustomListTest
    {
        [TestMethod]
        public void ConstructorShouldCreateInitializeTheArrayWithDeafaultCapacity()
        {
            var cutomList = new CustomList<int>();
            var expextedResult=2;
            var actualResult = cutomList.Capacity;
            Assert.AreEqual(expextedResult, actualResult);
        }
    }
}
