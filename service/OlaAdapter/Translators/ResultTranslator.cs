using OlaDatabase.Entities;
using OrienteeringTvResults.Common.Calculators;
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
            IList<Result> results = new List<Result>();
            foreach (var resultEntity in resultEntities)
            {
                results.Add(ToResultWithTimes(resultEntity));
            }

            results = ResultOrderer.OrderByStatusAndTime(results);
            results = OrdinalCalculator.AddOrdinal(results);
            results = ResultOrderer.OrderBySplitTimes(results);

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
                Status = OlaRunnerStatusTranslator.ToResultStatus(resultEntity.RunnerStatus),
                Club = resultEntity.Entry.Competitor.DefaultOrganisation.Name,
                ModifyDate = resultEntity.ModifyDate,
            };
        }
    }
}
