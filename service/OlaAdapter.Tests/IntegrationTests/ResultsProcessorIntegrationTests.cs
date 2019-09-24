using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using OlaAdapter.IntegrationTests.TestUtils;
using OlaDatabase.Entities;
using OlaDatabase.Session;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.OlaAdapter;
using System;

namespace OlaAdapter.Tests.IntegrationTests
{
    [TestClass]
    public class ResultsProcessorIntegrationTests
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

        [TestCleanup]
        public void TestCleanup()
        {
            //_sessionFactoryCreator.Close();
        }

        [TestMethod]
        public void TestGetClasses()
        {
            
            _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "D21");
            _session.Flush();

            var target = new OlaResultsProvider(new OlaConfiguration { EventId = 1, EventRaceId = 1 });
            var actual = target.GetClasses();

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("H21", actual[0].ShortName);
            Assert.AreEqual("D21", actual[1].ShortName);
        }

        [TestMethod]
        public void TestGetClassResults()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var organisation = _dataHelper.CreateOrganisation("The Club");
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "MrWinner", "Winnersson", organisation, "passed", new TimeSpan(1, 2, 3));
            _session.Flush();

            var target = CreateTarget();
            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
            Assert.AreEqual(1, actual.Results.Count);
            Assert.AreEqual("MrWinner", actual.Results[0].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[0].LastName);
            Assert.AreEqual("The Club", actual.Results[0].Club);
            Assert.AreEqual(new TimeSpan(1, 2, 3), actual.Results[0].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[0].Status);
            Assert.AreEqual(1, actual.Results[0].Ordinal);
        }

        [TestMethod]
        public void TestGetClassResultsCorrectOrdinalByTotalTime()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var organisation = _dataHelper.CreateOrganisation("The Club");
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbeAndra", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 2));
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbe1", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 1));
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbe3e", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 3));
            _session.Flush();

            var target = CreateTarget();

            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
            Assert.AreEqual(3, actual.Results.Count);

            Assert.AreEqual("gubbe1", actual.Results[0].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[0].LastName);
            Assert.AreEqual("The Club", actual.Results[0].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 1), actual.Results[0].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[0].Status);
            Assert.AreEqual(1, actual.Results[0].Ordinal);

            Assert.AreEqual("gubbeAndra", actual.Results[1].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[1].LastName);
            Assert.AreEqual("The Club", actual.Results[1].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 2), actual.Results[1].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[1].Status);
            Assert.AreEqual(2, actual.Results[1].Ordinal);

            Assert.AreEqual("gubbe3e", actual.Results[2].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[2].LastName);
            Assert.AreEqual("The Club", actual.Results[2].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 3), actual.Results[2].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[2].Status);
            Assert.AreEqual(3, actual.Results[2].Ordinal);
        }

        [TestMethod]
        public void TestGetClassResultsCorrectOrdinalByTotalTimeWhenEqual()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var organisation = _dataHelper.CreateOrganisation("The Club");
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbeAndra", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 2));
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbe1", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 1));
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbe4", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 3));
            _dataHelper.CreatePersonAndEntryAndResult(_eventEntity, raceClass, "gubbe3e", "Winnersson", organisation, "passed", new TimeSpan(1, 0, 2));
            _session.Flush();

            var target = CreateTarget();
            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
            Assert.AreEqual(4, actual.Results.Count);

            Assert.AreEqual("gubbe1", actual.Results[0].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[0].LastName);
            Assert.AreEqual("The Club", actual.Results[0].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 1), actual.Results[0].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[0].Status);
            Assert.AreEqual(1, actual.Results[0].Ordinal);

            Assert.AreEqual("gubbeAndra", actual.Results[1].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[1].LastName);
            Assert.AreEqual("The Club", actual.Results[1].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 2), actual.Results[1].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[1].Status);
            Assert.AreEqual(2, actual.Results[1].Ordinal);

            Assert.AreEqual("gubbe3e", actual.Results[2].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[2].LastName);
            Assert.AreEqual("The Club", actual.Results[2].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 2), actual.Results[2].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[2].Status);
            Assert.AreEqual(2, actual.Results[2].Ordinal);

            Assert.AreEqual("gubbe4", actual.Results[3].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[3].LastName);
            Assert.AreEqual("The Club", actual.Results[3].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 3), actual.Results[3].TotalTime);
            Assert.AreEqual(ResultStatus.Passed, actual.Results[3].Status);
            Assert.AreEqual(4, actual.Results[3].Ordinal);
        }

        [TestMethod]
        public void TestGetClassResultsLatestModifyDateFromSplitTime()
        {
            //Arrange
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var control100 = _dataHelper.CrateControl(_eventRace, 100);
            var control101 = _dataHelper.CrateControl(_eventRace, 101);
            var control102 = _dataHelper.CrateControl(_eventRace, 102);
            var control150 = _dataHelper.CrateControl(_eventRace, 150, "WTC");

            var course1 = _dataHelper.CreateCourse(_eventRace, "Course1");

            _dataHelper.CreateRaceClassCourse(raceClass, course1);

            _dataHelper.CreateCourseWaypointControl(course1, control100, 1);
            _dataHelper.CreateCourseWaypointControl(course1, control150, 2);
            _dataHelper.CreateCourseWaypointControl(course1, control101, 3);
            _dataHelper.CreateCourseWaypointControl(course1, control150, 4);
            _dataHelper.CreateCourseWaypointControl(course1, control102, 5);

            var organisation = _dataHelper.CreateOrganisation("The Club");
            var entry = _dataHelper.CreatePersonAndEntry(_eventEntity, "gubbeAndra", "Winnersson", organisation);
            var result = new ResultEntity("notActivated")
            {
                RaceClass = raceClass,
                Entry = entry,
                ModifyDate = new DateTime(2001, 1, 1),
            };
            _session.Save(result);

            var splitTimeControl150 = new SplitTimeControlEntity(_eventRace) { TimingControl = control150 } ;
            _session.Save(splitTimeControl150);
            var splitTime1 = new SplitTimeEntity(result, splitTimeControl150, 1, new DateTime(2001, 1, 3, 10, 1, 0), 60 * 100) { ModifyDate = new DateTime(2001, 1, 3, 10, 1, 0) };
            _session.Save(splitTime1);
            var splitTime2 = new SplitTimeEntity(result, splitTimeControl150, 2, new DateTime(2001, 1, 3, 10, 2, 0), 2 * 60 * 100) { ModifyDate = new DateTime(2001, 1, 3, 10, 2, 0) };
            _session.Save(splitTime2);

            _dataHelper.CreateRaceClassSplitTimeControl(raceClass, splitTimeControl150);
            _dataHelper.FlushAndClear();

            var target = CreateTarget();

            //Act
            var actual = target.GetClass(1);

            //Assert
            var actualResults = actual.Results;
            Assert.AreEqual(1, actualResults.Count);
            Assert.AreEqual("gubbeAndra", actual.Results[0].FirstName);
            Assert.AreEqual(new DateTime(2001, 1, 3, 10, 2, 0), actual.Results[0].ModifyDate);
            Assert.AreEqual(2, actual.Results[0].SplitTimes.Count);
            Assert.AreEqual(1, actual.Results[0].SplitTimes[0].PassedCount);
            Assert.AreEqual(2, actual.Results[0].SplitTimes[1].PassedCount);
        }

        private IResultsProvider CreateTarget()
        {
            return new OlaResultsProvider(new OlaConfiguration { EventId = 1, EventRaceId = 1 });
        }
    }
}
