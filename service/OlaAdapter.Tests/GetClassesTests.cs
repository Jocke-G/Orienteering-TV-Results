using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlaAdapter.IntegrationTests.TestUtils;
using OlaDatabase.Entities;
using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaAdapter;

namespace OlaAdapter.IntegrationTests
{
    [TestClass]
    public class GetClassesTests
    {
        private InMemoryTestHelper _util;
        private EventEntity _eventEntity;
        private EventRaceEntity _eventRace;

        [TestInitialize]
        public void TestInitialize()
        {
            _util = new InMemoryTestHelper();

            _eventEntity = _util.CreateEvent("TestTävling");
            _eventRace = _util.CreateEventRace(_eventEntity);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestGetClasses_NoClassesFound()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var actual = target.GetClasses();

            //Assert
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void TestGetClasses_ClassesFound()
        {
            //Arrange
            _util.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "D21");
            _util.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            _util.FlushAndClear();

            var target = CreateTarget();

            //Act
            var actual = target.GetClasses();

            //Assert
            Assert.AreEqual(2, actual.Count);

            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual("D21", actual[0].ShortName);
            Assert.AreEqual(null, actual[0].SplitControls);
            Assert.AreEqual(null, actual[0].Results);

            Assert.AreEqual(2, actual[1].Id);
            Assert.AreEqual("H21", actual[1].ShortName);
            Assert.AreEqual(null, actual[1].SplitControls);
            Assert.AreEqual(null, actual[1].Results);
        }

        private IResultsProvider CreateTarget()
        {
            var conf = new OlaConfiguration {
                EventId = _eventEntity.EventId,
                EventRaceId = _eventRace.EventRaceId,
            };
            return new OlaResultsProvider(conf);
        }
    }
}
