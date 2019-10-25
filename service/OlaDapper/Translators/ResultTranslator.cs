using OrienteeringTvResults.Common.Translators;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaDapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaDapper.Translators
{
    public static class ResultTranslator
    {
        public static IList<Result> ToContracts(this IList<ResultEntity> entities)
        {
            return entities
                .Select(x => x.ToContract())
                .ToList();
        }

        public static IList<Result> ToContractsWithoutTimes(this IList<ResultEntity> entities)
        {
            return entities
                .Select(x => x.ToContractWithoutTime())
                .ToList();
        }

        public static IList<Result> ToContractsWithSplitTimes(this IList<ResultEntity> entities, CourseEntity course)
        {
            return entities
                .Select(x => x
                    .ToContract()
                    .AddSplitTimes(x, course, entities))
                .ToList();
        }

        public static Result ToContractWithoutTime(this ResultEntity entity)
        {
            return new Result
            {
                FirstName = entity.FirstName,
                LastName = entity.FamilyName,
                Club = entity.Club,
                ClassName = entity.ClassName,
                ModifyDate = entity.ModifyDate,
                Status = OlaRunnerStatusTranslator.ToResultStatus(entity.RunnerStatus),
            };
        }
        
        public static Result ToContract(this ResultEntity entity)
        {
            return new Result
            {
                FirstName = entity.FirstName,
                LastName = entity.FamilyName,
                Club = entity.Club,
                ClassName = entity.ClassName,
                ModifyDate = entity.ModifyDate,
                Status = OlaRunnerStatusTranslator.ToResultStatus(entity.RunnerStatus),
                Ordinal = entity.Ordinal,

                StartTime = entity.StartTime,
                FinishTime = entity.FinishTime,
                TotalTime = OlaTimeSpanTranslator.ToTimeSpan(entity.TotalTime),
            };
        }

        public static Result AddSplitTimes(this Result contract, ResultEntity e, CourseEntity course, IList<ResultEntity> results)
        {
            contract.SplitTimes = e.SplitTimes.ToContracts(course, results);

            return contract;
        }

        public static IList<SplitTime> ToContracts(this IList<SplitTimeEntity> entities, CourseEntity course, IList<ResultEntity> results)
        {
            var splitTimes = new List<SplitTime>();

            var passedCountDictionary = new Dictionary<int, int>();
            foreach (var splitControl in course.SplitControls)
            {
                var SplitTimeControlId = splitControl.SplitTimeControlId;
                if (!passedCountDictionary.TryGetValue(SplitTimeControlId, out int passedCount))
                {
                    passedCount = 1;
                }

                var splitTime = entities
                    .Where(x => x.SplitTimeControlId == SplitTimeControlId
                    && x.PassedCount == passedCount)
                    .FirstOrDefault();

                if (splitTime == null)
                {
                    splitTimes.Add(new SplitTime
                    {
                        PassedCount = passedCount,
                    });
                }
                else
                {
                    var ordinal = 1 + results
                        .Where(x => x.SplitTimes
                            .Any(z => z.SplitTimeControlId == SplitTimeControlId
                                    && z.PassedCount == passedCount
                                    && z.SplitTime < splitTime.SplitTime)).Count();

                    splitTimes.Add(new SplitTime
                    {
                        Time = TimeSpan.FromSeconds(splitTime.SplitTime / 100),
                        PassedCount = splitTime.PassedCount,
                        Ordinal = ordinal,
                    });
                }
                passedCountDictionary[SplitTimeControlId] = passedCount + 1;
            }

            return splitTimes;
        }
    }
}
