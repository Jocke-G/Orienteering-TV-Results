using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using OlaDatabase;
using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.OlaAdapter;
using System.Collections.Generic;

namespace OlaAdapter.Tests.UnitTests
{
    [TestClass]
    public class ResultsProcessorTests
    {
        [TestMethod]
        public void TestGetClass()
        {
            var mockSession = new Mock<ISession>();
//            var mockSessionFactory = new Mock<ISessionFactory>();
//            mockSessionFactory.Setup(r => r.OpenSession()).Returns(mockSession.Object);
            var mockSessionFactoryCreator = new Mock<ISessionFactoryCreator>();
            mockSessionFactoryCreator.Setup(x => x.GetSession()).Returns(mockSession.Object);

            SessionFactoryHelper.Initialize(mockSessionFactoryCreator.Object);

            var mockResultsRepository = new Mock<IRaceClassRepository>();
            mockResultsRepository.Setup(x => x.GetByEventRaceIdAndId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((i1, i2) =>
                new RaceClassEntity {
                    EventClass = new EventClassEntity
                    {
                        EventClassId = 1,
                        ShortName = "H21",
                    }
                });
            RepositoryContainer.RaceClassRepository = mockResultsRepository.Object;

            var mockResultRepository = new Mock<IResultRepository>();
            mockResultRepository.Setup(x => x.GetBy(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((i1, i2) => new List<ResultEntity>());
            RepositoryContainer.ResultRepository = mockResultRepository.Object;

            IResultsProcessor target = new ResultsProcessor(new DatabaseConfiguration());
            var actual = target.GetClass(1, 1, 1);

            Assert.AreEqual("H21", actual.ShortName);
        }
    }
}
