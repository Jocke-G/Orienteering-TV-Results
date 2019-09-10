using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlaDatabase;
using OlaDatabase.Entities;
using OrienteeringTvResults.OlaAdapter;

namespace OlaAdapter.Tests.IntegrationTests
{
    [TestClass]
    public class ResultsProcessorIntegrationTests
    {
        [TestMethod]
        public void TestGetClassResults()
        {
            var sessionFactoryCreator = new InMemorySessionFactoryCreator();
            SessionFactoryHelper.Initialize(sessionFactoryCreator);
            sessionFactoryCreator.OpenSession();

            var session = sessionFactoryCreator.GetSession();

            var eventEntity = new EventEntity{
                Name = "TestTävling",
                EventForm = "IndSingleDay",
            };
            session.Save(eventEntity);

            EventRaceEntity eventRace = new EventRaceEntity
            {
                EventRaceId = 1,
                RaceStatus = "notActivated",
                RaceLightCondition = "Day",
                RaceDistance = "Middle",
                Event = eventEntity,
            };
            session.Save(eventRace);

            var eventClass = new EventClassEntity
            {
                ClassStatus = "enterable",
                Name = "H21",
                ShortName = "H21",
                Sex = "M",
            };
            session.Save(eventClass);

            session.Save(new RaceClassEntity
            {
                RaceClassName = "H21",
                EventClass = eventClass,
                EventRace = eventRace,
            });
            session.Flush();

            var target = new ResultsProcessor();
            var actual = target.GetClass(1, 1, 1);

            Assert.AreEqual("H21", actual.ShortName);
        }
    }
}
