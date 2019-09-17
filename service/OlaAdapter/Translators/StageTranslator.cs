using System.Collections.Generic;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.OlaAdapter.Translators
{
    static class StageTranslator
    {
        internal static IList<CompetitionStage> ToStages(IEnumerable<EventRaceEntity> eventRaces)
        {
            var stages = new List<CompetitionStage>();
            foreach (var eventRace in eventRaces)
            {
                stages.Add(ToStage(eventRace));
            }
            return stages;
        }

        internal static CompetitionStage ToStage(EventRaceEntity eventRace)
        {
            return new CompetitionStage
            {
                Id = eventRace.EventRaceId,
                Name = eventRace.Name,
            };
        }
    }
}
