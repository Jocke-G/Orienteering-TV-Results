using MeosAdapter.IntegrationTests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrienteeringTvResults.MeosAdapter;
using OrienteeringTvResults.Model;

namespace MeosAdapter.IntegrationTests
{
    [TestClass]
    public class GetClassesTests
    {
        private InMemoryTestHelper _util;

        [TestInitialize]
        public void TestInitialize()
        {
            _util = new InMemoryTestHelper();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _util.Dispose();
            _util = null;
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
            _util.CreateClass("D21");
            _util.CreateClass("H21");
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
            var conf = new MeosConfiguration();
            return new MeosResultsProvider(conf);
        }
    }
}
