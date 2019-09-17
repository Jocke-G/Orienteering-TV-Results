using OlaDatabase.Entities;
using OrienteeringTvResults.Model;
using System.Collections.Generic;
using System.Linq;

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
                SplitControls = ToSplitControls(raceClass),
            };

            return competitionClass;
        }

        internal static CompetitionClass ToClassWithResults(RaceClassEntity raceClass)
        {
            var competitionClass = ToClass(raceClass);
            if (raceClass.EventClass.NoTimePresentation)
            {
                competitionClass.Results = ResultTranslator.ToResultsWithoutTime(raceClass.Results);
            }
            else
            {
                competitionClass.Results = ResultTranslator.ToResultsWithTimes(raceClass.Results);
            }
            return competitionClass;
        }
        
        internal static IList<SplitControl> ToSplitControls(RaceClassEntity raceClass)
        {
            var splitControls = new List<SplitControl>();
            foreach (var splitControl in raceClass.RaceClassSplitTimeControls.OrderBy(x => x.Ordered))
            {
                splitControls.Add(new SplitControl
                {
                    Name = splitControl.Id.SplitTimeControl.Name,
                });
            }
            return splitControls;
        }
    }
}
