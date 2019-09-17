using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using OlaDatabase;
using OlaDatabase.Entities;
using OlaDatabase.Session;
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
        private DataHelper _dataHelper;
        private EventEntity _eventEntity;
        private EventRaceEntity _eventRace;

        [TestInitialize]
        public void TestInitialize()
        {
            _sessionFactoryCreator = new InMemorySessionFactoryCreator();
            SessionFactoryHelper.Initialize(_sessionFactoryCreator);
            _sessionFactoryCreator.OpenSession();
            _session = _sessionFactoryCreator.GetSession();
            _dataHelper = new DataHelper(_session);

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


            var target = new ResultsProcessor(new DatabaseConfiguration { Competition = 1, Stage = 1 });
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

            var target = new ResultsProcessor(new DatabaseConfiguration { Competition = 1, Stage = 1 });
            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
            Assert.AreEqual(1, actual.Results.Count);
            Assert.AreEqual("MrWinner", actual.Results[0].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[0].LastName);
            Assert.AreEqual("The Club", actual.Results[0].Club);
            Assert.AreEqual(new TimeSpan(1, 2, 3), actual.Results[0].TotalTime);
            Assert.AreEqual("passed", actual.Results[0].Status);
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

            var target = new ResultsProcessor(new DatabaseConfiguration { Competition = 1, Stage = 1 });
            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
            Assert.AreEqual(3, actual.Results.Count);

            Assert.AreEqual("gubbe1", actual.Results[0].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[0].LastName);
            Assert.AreEqual("The Club", actual.Results[0].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 1), actual.Results[0].TotalTime);
            Assert.AreEqual("passed", actual.Results[0].Status);
            Assert.AreEqual(1, actual.Results[0].Ordinal);

            Assert.AreEqual("gubbeAndra", actual.Results[1].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[1].LastName);
            Assert.AreEqual("The Club", actual.Results[1].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 2), actual.Results[1].TotalTime);
            Assert.AreEqual("passed", actual.Results[1].Status);
            Assert.AreEqual(2, actual.Results[1].Ordinal);

            Assert.AreEqual("gubbe3e", actual.Results[2].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[2].LastName);
            Assert.AreEqual("The Club", actual.Results[2].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 3), actual.Results[2].TotalTime);
            Assert.AreEqual("passed", actual.Results[2].Status);
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

            var target = new ResultsProcessor(new DatabaseConfiguration { Competition = 1, Stage = 1 });
            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
            Assert.AreEqual(4, actual.Results.Count);

            Assert.AreEqual("gubbe1", actual.Results[0].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[0].LastName);
            Assert.AreEqual("The Club", actual.Results[0].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 1), actual.Results[0].TotalTime);
            Assert.AreEqual("passed", actual.Results[0].Status);
            Assert.AreEqual(1, actual.Results[0].Ordinal);

            Assert.AreEqual("gubbeAndra", actual.Results[1].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[1].LastName);
            Assert.AreEqual("The Club", actual.Results[1].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 2), actual.Results[1].TotalTime);
            Assert.AreEqual("passed", actual.Results[1].Status);
            Assert.AreEqual(2, actual.Results[1].Ordinal);

            Assert.AreEqual("gubbe3e", actual.Results[2].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[2].LastName);
            Assert.AreEqual("The Club", actual.Results[2].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 2), actual.Results[2].TotalTime);
            Assert.AreEqual("passed", actual.Results[2].Status);
            Assert.AreEqual(2, actual.Results[2].Ordinal);

            Assert.AreEqual("gubbe4", actual.Results[3].FirstName);
            Assert.AreEqual("Winnersson", actual.Results[3].LastName);
            Assert.AreEqual("The Club", actual.Results[3].Club);
            Assert.AreEqual(new TimeSpan(1, 0, 3), actual.Results[3].TotalTime);
            Assert.AreEqual("passed", actual.Results[3].Status);
            Assert.AreEqual(4, actual.Results[3].Ordinal);
        }
    }
}
