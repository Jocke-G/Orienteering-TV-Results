using OrienteeringTvResults.Common.Translators;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaDapper.Entities;
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

        public static Result ToContract(this ResultEntity entity)
        {
            return new Result
            {
                FirstName = entity.FirstName,
                LastName = entity.FamilyName,
                Club = entity.Club,
                ModifyDate = entity.ModifyDate,
                StartTime = entity.StartTime,
                Status = OlaRunnerStatusTranslator.ToResultStatus(entity.RunnerStatus),
                TotalTime = OlaTimeSpanTranslator.ToTimeSpan(entity.TotalTime),
            };
        }
    }
}
