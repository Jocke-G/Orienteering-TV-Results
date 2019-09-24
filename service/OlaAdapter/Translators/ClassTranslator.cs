using OlaDatabase;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaAdapter.EagerFetchers;
using System.Collections.Generic;

namespace OrienteeringTvResults.OlaAdapter.Translators
{
    class ClassTranslator
    {
        internal static IList<CompetitionClass> ToClasses(IEnumerable<RaceClassEntity> raceClasses)
        {
            var classes = new List<CompetitionClass>();
            foreach (var raceClass in raceClasses)
            {
                classes.Add(ToClass(raceClass));
            }
            return classes;
        }

        internal static CompetitionClass ToClass(RaceClassEntity raceClass)
        {
            var competitionClass = new CompetitionClass
            {
                Id = raceClass.EventClass.EventClassId,
                ShortName = raceClass.EventClass.ShortName,
                NoTimePresentation = raceClass.EventClass.NoTimePresentation,
            };

            return competitionClass;
        }

        internal static CompetitionClass ToClassWithResults(RaceClassEntity raceClass)
        {
            var competitionClass = ToClass(raceClass).AddSplitControls(raceClass);

            var results = RepositoryContainer.ResultRepository
                .GetByRaceClassId(raceClass.EventRace.Event.EventId, raceClass.EventRace.EventRaceId, raceClass.RaceClassId)
                .EagerlyFetch(x => x.Entry)
                .ThenEagerlyFetch(x => x.Competitor)
                .ThenEagerlyFetch(x => x.DefaultOrganisation)
                .EagerlyFetchMany(x => x.SplitTimes)
                .ThenEagerlyFetch(x => x.Id)
                .ThenEagerlyFetch(x => x.SplitTimeControl)
                .ThenEagerlyFetch(x => x.TimingControl);

            if (raceClass.EventClass.NoTimePresentation)
            {
                competitionClass.Results = ResultTranslator.ToResultsWithoutTime(results);
            }
            else
            {
                competitionClass.Results = ResultTranslator.ToResultsWithTimes(results);
            }
            return competitionClass;
        }
    }
}
