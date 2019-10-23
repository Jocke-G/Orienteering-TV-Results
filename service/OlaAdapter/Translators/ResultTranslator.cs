using OlaDatabase.Entities;
using OrienteeringTvResults.Common.Translators;
using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter.Translators
{
    static class ResultTranslator
    {
        internal static IList<Result> ToResultsWithoutTime(IEnumerable<ResultEntity> resultEntitiess)
        {
            var results = new List<Result>();

            foreach (var resultEntity in resultEntitiess)
            {
                results.Add(ToResultWithoutTime(resultEntity));
            }

            return results.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }

        internal static IList<Result> ToResultsWithTimes(IEnumerable<ResultEntity> resultEntities)
        {
            List<Result> results = new List<Result>();
            foreach (var resultEntity in resultEntities)
            {
                results.Add(ToResultWithTimes(resultEntity));
            }

            results = OrderByStatusAndTime(results);
            results = AddOrdinal(results);
            results.Sort();

            return results;
        }

        private static Result ToResultWithTimes(ResultEntity resultEntity)
        {
            return new Result
            {
                FirstName = resultEntity.Entry.Competitor.FirstName,
                LastName = resultEntity.Entry.Competitor.FamilyName,
                StartTime = resultEntity.StartTime,
                Status = OlaRunnerStatusTranslator.ToResultStatus(resultEntity.RunnerStatus),
                Club = resultEntity.Entry.Competitor.DefaultOrganisation.Name,
                ModifyDate = CalculateModifyDateIncludingSplitTimes(resultEntity),
                TotalTime = OlaTimeSpanTranslator.ToTimeSpan(resultEntity.TotalTime),
                SplitTimes = SplitTranslator.ToSplitTimes(resultEntity),
            };
        }

        private static DateTime CalculateModifyDateIncludingSplitTimes(ResultEntity resultEntity)
        {
            if (!resultEntity.SplitTimes.Any())
            {
                return resultEntity.ModifyDate;
            }
            return new DateTime(Math.Max(resultEntity.ModifyDate.Ticks, resultEntity.SplitTimes.Max(x => x.ModifyDate).Ticks));
        }

        private static Result ToResultWithoutTime(ResultEntity resultEntity)
        {
            return new Result
            {
                FirstName = resultEntity.Entry.Competitor.FirstName,
                LastName = resultEntity.Entry.Competitor.FamilyName,
                StartTime = resultEntity.StartTime,
                Status = OlaRunnerStatusTranslator.ToResultStatus(resultEntity.RunnerStatus),
                Club = resultEntity.Entry.Competitor.DefaultOrganisation.Name,
                ModifyDate = resultEntity.ModifyDate,
            };
        }

        private static List<Result> OrderByStatusAndTime(List<Result> results)
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

        private static List<Result> AddOrdinal(List<Result> results)
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

                if (results.LastIndexOf(result) != i && results[i + 1].TotalTime > result.TotalTime)
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
