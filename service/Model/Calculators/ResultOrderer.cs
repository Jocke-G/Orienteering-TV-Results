using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.Common.Calculators
{
    public class ResultOrderer
    {
        public static IList<Result> OrderByStatusAndTime(IList<Result> results)
        {
            var preferences = new Dictionary<ResultStatus, int> {
                { ResultStatus.Finished, 1 },
                { ResultStatus.Passed, 1 },
                { ResultStatus.NotFinishedYet, 1 },
                { ResultStatus.NotPassed, 2 },
                { ResultStatus.NotStarted, 3 },
            };

            return results.OrderBy(item => preferences.GetValueOrDefault(item.Status)).ThenBy(x => x.TotalTime).ToList();
        }

        public static IList<Result> OrderByName(IList<Result> results)
        {
            return results
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();
        }

        public static IList<Result> OrderBySplitTimes(IList<Result> results)
        {
            var sortableResult = results.ToList();
            sortableResult.Sort();
            return sortableResult;
        }
    }
}
