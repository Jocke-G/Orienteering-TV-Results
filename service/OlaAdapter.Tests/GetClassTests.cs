using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using OlaAdapter.IntegrationTests.TestUtils;
using OlaDatabase.Entities;
using OlaDatabase.Session;
using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaAdapter;

namespace OlaAdapter.Tests.IntegrationTests
{
    [TestClass]
    public class GetClassTests
    {
        private ISessionFactoryCreator _sessionFactoryCreator;
        private ISession _session;
        private InMemoryTestHelper _dataHelper;
        private EventEntity _eventEntity;
        private EventRaceEntity _eventRace;

        [TestInitialize]
        public void TestInitialize()
        {
            _sessionFactoryCreator = new InMemorySessionFactoryCreator();
            SessionFactoryHelper.Initialize(_sessionFactoryCreator);
            _sessionFactoryCreator.OpenSession();
            _session = _sessionFactoryCreator.GetSession();
            _dataHelper = new InMemoryTestHelper(_session);

            _eventEntity = _dataHelper.CreateEvent("TestTävling");
            _eventRace = _dataHelper.CreateEventRace(_eventEntity);
        }

        [TestMethod]
        public void TestGetClassWithSplitTimeControls()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");

            var control100 = _dataHelper.CrateControl(_eventRace, 100);
            var control101 = _dataHelper.CrateControl(_eventRace, 101, "WTC");
            var control102 = _dataHelper.CrateControl(_eventRace, 102);
            var control103 = _dataHelper.CrateControl(_eventRace, 103);

            var course1 = _dataHelper.CreateCourse(_eventRace, "Course1");

            _dataHelper.CreateRaceClassCourse(raceClass, course1);

            _dataHelper.CreateCourseWaypointControl(course1, control100, 1);
            _dataHelper.CreateCourseWaypointControl(course1, control101, 2);
            _dataHelper.CreateCourseWaypointControl(course1, control102, 3);
            _dataHelper.CreateCourseWaypointControl(course1, control101, 4);
            _dataHelper.CreateCourseWaypointControl(course1, control103, 5);

            var splitTimeControl = _dataHelper.CreateSplitTimeControl(_eventRace, control101, "Radio");

            _dataHelper.CreateRaceClassSplitTimeControl(raceClass, splitTimeControl);

            _dataHelper.FlushAndClear();
            
            var target = CreateTarget();
            var actual = target.GetClass(1);

            Assert.AreEqual(2, actual.SplitControls.Count);
            Assert.AreEqual("Radio", actual.SplitControls[0].Name);
            Assert.AreEqual("Radio", actual.SplitControls[1].Name);
        }

        private IResultsProvider CreateTarget()
        {
            return new OlaResultsProvider(new OlaConfiguration { EventId = 1, EventRaceId = 1 });
        }
    }
}
