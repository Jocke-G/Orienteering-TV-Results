using OrienteeringTvResults.Model;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.Common.Calculators
{
    public static class OrdinalCalculator
    {
        public static IList<Result> AddOrdinal(IList<Result> results)
        {
            var ordinal = 1;
            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                if (result.Status != ResultStatus.Passed && result.Status != ResultStatus.Finished)
                {
                    break;
                }

                result.Ordinal = ordinal;

                if (results.ToList().LastIndexOf(result) != i && results[i + 1].TotalTime > result.TotalTime)
                {
                    ordinal++;
                }
                else if (i > 0 && results[i - 1].TotalTime != result.TotalTime)
                {
                    ordinal = i + 1;
                }
                result.Ordinal = ordinal;
            }

            return results;
        }
    }
}
