using System.Collections.Generic;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.OlaAdapter.Translators
{
    static class CompetitionTranslator
    {
        internal static IList<Competition> ToCompetitions(IEnumerable<EventEntity> eventEntities)
        {
            var competitions = new List<Competition>();
            foreach (EventEntity eventEntity in eventEntities)
            {
                competitions.Add(ToCompetition(eventEntity));
            }
            return competitions;
        }

        internal static Competition ToCompetition(EventEntity eventEntity)
        {
            return new Competition
            {
                Id = eventEntity.EventId,
                Name = eventEntity.Name,
            };
        }

        internal static Competition ToCompetitionWithStages(EventEntity eventEntity)
        {
            var competition = ToCompetition(eventEntity);
            competition.CompetitionStages = StageTranslator.ToStages(eventEntity.EventRaces);
            return competition;
        }
    }
}
