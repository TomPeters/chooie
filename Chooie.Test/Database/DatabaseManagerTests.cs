using System.Collections.Generic;
using Chooie.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EasyAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Chooie.Test.Database
{
    [TestClass]
    public class DatabaseManagerTests
    {
        private string _jsonWrittenToDatabase;

        [TestInitialize]
        public void Setup()
        {
            _jsonWrittenToDatabase = null;
        }

        [TestMethod]
        public void GetDatabaseObject_DatabaseEmpty_ReturnsNull()
        {
            var sut = CreateDatabaseManager(CreateMockDatabaseAccessor(string.Empty));
            sut.GetDatabaseObject<TestDatabaseObject>("something").ShouldBeNull();
        }

        [TestMethod]
        public void GetDatabaseObject_DatabaseContainsData_ReturnsDeserializedData()
        {
            const string json = @"{
	            'databaseObject': {
		            'StringProperty': 'string',
		            'IntProperty': 5,
		            'List': [true, false, false, true, false]
	            }
            }";
            var sut = CreateDatabaseManager(CreateMockDatabaseAccessor(json));
            var result = sut.GetDatabaseObject<TestDatabaseObject>("databaseObject");
            result.ShouldNotBeNull();
            result.StringProperty.ShouldBe("string");
            result.IntProperty.ShouldBe(5);
            result.List.ShouldMatch(new[]
                {
                    true, false, false, true, false
                });
        }

        [TestMethod]
        public void GetDatabaseObject_ObjectDoesNotExistInDatabase_ReturnsNull()
        {
            const string json = @"{'someting':1}";
            var sut = CreateDatabaseManager(CreateMockDatabaseAccessor(json));
            sut.GetDatabaseObject<TestDatabaseObject>("databaseObject").ShouldBeNull();
        }

        [TestMethod]
        public void SaveDatabaseObject_ObjectIsNull_ObjectIsRemovedFromJson()
        {
            const string json = @"{'databaseObject': true}";
            IDatabaseAccessor mockDatabaseAccessor = CreateMockDatabaseAccessor(json);
            var sut = CreateDatabaseManager(mockDatabaseAccessor);
            sut.SaveDatabaseObject("databaseObject", null);
            Mock.Get(mockDatabaseAccessor).Verify(da => da.WriteToDatabase(@"{}"), Times.Once());
        }

        [TestMethod]
        public void SaveDatabaseObject_ObjectInPopulated_ObjectIsSavedToDatabase()
        {
            const string json = @"{'someOtherObject': true}";

            IDatabaseAccessor mockDatabaseAccessor = CreateMockDatabaseAccessor(json);
            var sut = CreateDatabaseManager(mockDatabaseAccessor);
            var testDatabaseObject = new TestDatabaseObject
                {
                    IntProperty = 5, StringProperty = "String", List = new[]
                        {
                            true, false, true
                        }
                };
            sut.SaveDatabaseObject("databaseObject", testDatabaseObject);
            Mock.Get(mockDatabaseAccessor).Verify(da => da.WriteToDatabase(It.IsAny<string>()), Times.Once());

            var savedJsonTree = JObject.Parse(_jsonWrittenToDatabase);
            savedJsonTree["someOtherObject"].ToObject<bool>().ShouldBe(true);
            var savedTestDatabaseObject = JsonConvert.DeserializeObject<TestDatabaseObject>(savedJsonTree["databaseObject"].ToString());
            savedTestDatabaseObject.StringProperty.ShouldBe(testDatabaseObject.StringProperty);
            savedTestDatabaseObject.IntProperty.ShouldBe(testDatabaseObject.IntProperty);
            savedTestDatabaseObject.List.ShouldMatch(testDatabaseObject.List.ToArray());
        }

        [TestMethod]
        public void SaveThenLoadDatabaseObject_ObjectIsSavedAndLoadedCorrectly()
        {
            const string json = @"{'someOtherObject': true}";

            IDatabaseAccessor mockDatabaseAccessor = CreateMockDatabaseAccessor(json);
            var sut = CreateDatabaseManager(mockDatabaseAccessor);
            var testDatabaseObject = new TestDatabaseObject
            {
                IntProperty = 5,
                StringProperty = "String",
                List = new[]
                        {
                            true, false, true
                        }
            };
            sut.SaveDatabaseObject("databaseObject", testDatabaseObject);

            var sut2 = CreateDatabaseManager(CreateMockDatabaseAccessor(_jsonWrittenToDatabase));

            var savedAndLoadedTestDatabaseObject = sut2.GetDatabaseObject<TestDatabaseObject>("databaseObject");
            savedAndLoadedTestDatabaseObject.StringProperty.ShouldBe(testDatabaseObject.StringProperty);
            savedAndLoadedTestDatabaseObject.IntProperty.ShouldBe(testDatabaseObject.IntProperty);
            savedAndLoadedTestDatabaseObject.List.ShouldMatch(testDatabaseObject.List.ToArray());
        }

        private IDatabaseAccessor CreateMockDatabaseAccessor(string databaseJson = null)
        {
            var mockDatabaseAccessor = new Mock<IDatabaseAccessor>();
            mockDatabaseAccessor.Setup(dba => dba.ReadDatabase()).Returns(databaseJson);
            mockDatabaseAccessor.Setup(dba => dba.WriteToDatabase(It.IsAny<string>()))
                                .Callback((string json) => _jsonWrittenToDatabase = json);
            return mockDatabaseAccessor.Object;
        }

        private IDatabaseManager CreateDatabaseManager(IDatabaseAccessor databaseAccessor)
        {
            return new DatabaseManager(databaseAccessor);
        }

        private class TestDatabaseObject
        {
            public string StringProperty { get; set; }
            public int IntProperty { get; set; }
            public IList<bool> List { get; set; }
        }
    }
}
