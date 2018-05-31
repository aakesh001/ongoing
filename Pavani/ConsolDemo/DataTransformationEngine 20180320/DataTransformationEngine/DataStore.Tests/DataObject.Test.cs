using NUnit.Framework;
using DataStore;
using System.Collections.Generic;

namespace DataStore.Tests
{

    [TestFixture]
    public class DataStoreTests
    {
        [Test]
        public void MapTests()
        {
            // Arrange
            var dataObject = new DataObject();
            var action = String.Empty;
            var parameters = Dictionary<string, String>;

            action = "copy";
            parameters.Add("sourceFieldName", "testSource");
            parameters.Add("targetFieldName", "testTarget");
            //Act
            dataObject.Map(action, parameters);
            

            // Assert
            Assert.That(actual, Is.EqualTo(0));
        }
    }
}