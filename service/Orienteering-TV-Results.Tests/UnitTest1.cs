using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;

namespace Orienteering_TV_Results.Tests
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void ResultTest_SortByStatus()
        {
            //Arrange
            var actual = new List<Result>
            {
                new Result
                {
                    Status = ResultStatus.Passed,
                },
                new Result
                {
                    Status = ResultStatus.NotPassed,
                },
                                new Result
                {
                    Status = ResultStatus.NotStarted,
                },
            };

            //Act
            actual.Sort();

            //Assert
            Assert.AreEqual(ResultStatus.Passed, actual[0].Status);
            Assert.AreEqual(ResultStatus.NotPassed, actual[1].Status);
            Assert.AreEqual(ResultStatus.NotStarted, actual[2].Status);
        }

        [TestMethod]
        public void ResultTest_SortByTime()
        {
            //Arrange
            var actual = new List<Result>
            {
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 2, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 1, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 3, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 5, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 5, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 6, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 7, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 4, 0),
                },
                new Result
                {
                    Status = ResultStatus.Passed,
                    TotalTime = new TimeSpan(0, 3, 0),
                },
            };

            //Act
            actual.Sort();

            //Assert
            Assert.AreEqual(new TimeSpan(0, 1, 0), actual[0].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 2, 0), actual[1].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 3, 0), actual[2].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 3, 0), actual[3].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 4, 0), actual[4].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 5, 0), actual[5].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 5, 0), actual[6].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 6, 0), actual[7].TotalTime);
            Assert.AreEqual(new TimeSpan(0, 7, 0), actual[8].TotalTime);
        }

        [TestMethod]
        public void ResultTest_SortBySplitTime()
        {
            //Arrange
            var actual = new List<Result>
            {
                CreateResult(new TimeSpan?[]{ null, null, null }, null, ResultStatus.NotFinishedYet, "Gubbe7"),
                CreateResult(new TimeSpan?[]{ new TimeSpan(0, 1, 0), null, null }, null, ResultStatus.NotFinishedYet, "Gubbe1"),
                CreateResult(new TimeSpan?[]{ new TimeSpan(0, 2, 0), new TimeSpan(0, 5, 0), new TimeSpan(0, 6, 0) }, new TimeSpan(0, 7, 0), ResultStatus.Passed, "Gubbe3"),
                CreateResult(new TimeSpan?[]{ new TimeSpan(0, 4, 0), new TimeSpan(0, 6, 0), new TimeSpan(0, 8, 0) }, new TimeSpan(0, 10, 0), ResultStatus.Passed, "Gubbe4"),
                CreateResult(new TimeSpan?[]{ new TimeSpan(0, 3, 0), new TimeSpan(0, 4, 0), null }, null, ResultStatus.NotFinishedYet, "Gubbe2"),
                CreateResult(new TimeSpan?[]{ new TimeSpan(0, 3, 0), new TimeSpan(0, 7, 0), new TimeSpan(0, 8, 0) }, null, ResultStatus.Passed, "Gubbe5"),
                CreateResult(new TimeSpan?[]{ new TimeSpan(0, 7, 0), new TimeSpan(0, 8, 0), new TimeSpan(0, 9, 0) }, new TimeSpan(0, 100, 0), ResultStatus.Passed, "Gubbe6"),
            };

            //Act
            actual.Sort();

            //Assert
            Assert.AreEqual("Gubbe1", actual[0].FirstName);
            Assert.AreEqual("Gubbe2", actual[1].FirstName);
            Assert.AreEqual("Gubbe3", actual[2].FirstName);
            Assert.AreEqual("Gubbe4", actual[3].FirstName);
            Assert.AreEqual("Gubbe5", actual[4].FirstName);
            Assert.AreEqual("Gubbe6", actual[5].FirstName);
            Assert.AreEqual("Gubbe7", actual[6].FirstName);
        }

        private Result CreateResult(TimeSpan?[] splits, TimeSpan? totalTime, ResultStatus status, string name)
        {
            var splitTimes = new List<SplitTime>();
            foreach(var split in splits)
            {
                splitTimes.Add(new SplitTime { Time = split });
            }
            return new Result
            {
                FirstName = name,
                SplitTimes = splitTimes,
                TotalTime = totalTime,
                Status = status
            };
        }
    }
}
