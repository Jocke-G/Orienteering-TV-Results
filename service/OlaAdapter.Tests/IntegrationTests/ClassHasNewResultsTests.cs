using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using OlaDatabase.Entities;
using OlaDatabase.Session;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.OlaAdapter;
using System;

namespace OlaAdapter.Tests.IntegrationTests
{
    [TestClass]
    public class ClassHasNewResultsTests
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

            _eventEntity = _dataHelper.CreateEvent("TestCompetition");
            _eventRace = _dataHelper.CreateEventRace(_eventEntity);
        }

        [TestMethod]
        public void ClassHasNewResultsTests_ReturnsTrueIfResultChanged()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var organisation = _dataHelper.CreateOrganisation("The Club");
            var entry = _dataHelper.CreatePersonAndEntry(_eventEntity, "gubbeAndra", "Winnersson", organisation);
            var result = new ResultEntity("notActivated")
            {
                RaceClass = raceClass,
                Entry = entry,
                ModifyDate = new DateTime(2001, 1, 1),
            };
            _session.Save(result);
            _dataHelper.ClearAndFlush();

            var target = CreateTarget();

            var actual = target.ClassHasNewResults(1, new DateTime(2000, 1, 1));

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ClassHasNewResultsTests_ReturnsFalseIfResultChangedEarlier()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var organisation = _dataHelper.CreateOrganisation("The Club");
            var entry = _dataHelper.CreatePersonAndEntry(_eventEntity, "gubbeAndra", "Winnersson", organisation);
            var result = new ResultEntity("notActivated")
            {
                RaceClass = raceClass,
                Entry = entry,
                ModifyDate = new DateTime(2001, 1, 1),
            };
            _session.Save(result);
            _dataHelper.ClearAndFlush();

            var target = CreateTarget();

            var actual = target.ClassHasNewResults(1, new DateTime(2002, 1, 1));

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ClassHasNewResultsTests_ReturnsTrueIfSplitTimeChanged()
        {
            var raceClass = _dataHelper.CreateEventClassAndRaceClass(_eventEntity, _eventRace, "H21");
            var organisation = _dataHelper.CreateOrganisation("The Club");
            var entry = _dataHelper.CreatePersonAndEntry(_eventEntity, "gubbeAndra", "Winnersson", organisation);
            var result = new ResultEntity("notActivated")
            {
                RaceClass = raceClass,
                Entry = entry,
                ModifyDate = new DateTime(2001, 1, 1),
            };
            _session.Save(result);
            var splitTimeControl = new SplitTimeControlEntity(_eventRace);
            _session.Save(splitTimeControl);
            var splitTime = new SplitTimeEntity(result, splitTimeControl, 1, new DateTime(2001, 1, 3), 500) { ModifyDate = new DateTime(2001, 1, 3) };
            _session.Save(splitTime);
            _dataHelper.ClearAndFlush();

            var target = CreateTarget();

            var actual = target.ClassHasNewResults(1, new DateTime(2001, 1, 2));

            Assert.IsTrue(actual);
        }

        private IResultsProcessor CreateTarget()
        {
            return new ResultsProcessor(new DatabaseConfiguration { Competition = 1, Stage = 1 });
        }
    }
}
