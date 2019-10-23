using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using OlaDatabase;
using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using OlaDatabase.Session;
using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaAdapter;

namespace OlaAdapter.Tests.UnitTests
{
    [TestClass]
    public class ResultsProcessorTests
    {
        [TestMethod]
        public void TestGetClass()
        {
            var mockSession = new Mock<ISession>();
            var mockSessionFactoryCreator = new Mock<ISessionFactoryCreator>();
            mockSessionFactoryCreator.Setup(x => x.GetSession()).Returns(mockSession.Object);

            SessionFactoryHelper.Initialize(mockSessionFactoryCreator.Object);

            var mockResultsRepository = new Mock<IRaceClassRepository>();
            mockResultsRepository.Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int, int>((i1, i2, i3) =>
                {
                    var @event = new EventEntity();
                    var eventRace = new EventRaceEntity(@event);
                    @event.EventRaces.Add(eventRace);
                    return new RaceClassEntity
                    {
                        EventRace = eventRace,
                        EventClass = new EventClassEntity
                        {
                            EventClassId = 1,
                            ShortName = "H21",
                        },
                    };
                }
            );
        RepositoryContainer.RaceClassRepository = mockResultsRepository.Object;

            IResultsProvider target = new OlaResultsProvider(new OlaConfiguration { EventId = 1, EventRaceId = 1 });
            var actual = target.GetClass(1);

            Assert.AreEqual("H21", actual.ShortName);
        }
    }
}
