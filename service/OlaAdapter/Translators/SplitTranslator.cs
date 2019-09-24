using OlaDatabase.Entities;
using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter.Translators
{
    internal static class SplitTranslator
    {
        public static CompetitionClass AddSplitControls(this CompetitionClass competitionClass, RaceClassEntity raceClass)
        {
            var splitControls = ToSplitControls(raceClass);
            competitionClass.SplitControls = splitControls;
            return competitionClass;
        }

        internal static IList<SplitControl> ToSplitControls(RaceClassEntity raceClass)
        {
            var splitControls = new List<SplitControl>();
            var olaSplitControls = ControlsMarkedAsSplit(raceClass);
            foreach (var splitControl in olaSplitControls)
            {
                splitControls.Add(new SplitControl
                {
                    Name = splitControl.SplitTimeControls.Single().Name,
                });
            }
            return splitControls;
        }

        internal static IEnumerable<ControlEntity> ControlsMarkedAsSplit(RaceClassEntity raceClass)
        {
            var raceClassCourses = raceClass.RaceClassCourses;
            if (raceClassCourses.Count() != 1)
            {
                return new List<ControlEntity>();
            }
            return raceClassCourses.Single().Id.Course.CoursesWayPointControls
                .Where(x => x.Id.Control.TypeCode == "WTC")
                .Select(x => x.Id.Control);
        }

        internal static IList<SplitTime> ToSplitTimes(ResultEntity resultEntity)
        {
            var splitTimes = new List<SplitTime>();
            var controlsMarkedAsSplitTime = SplitTranslator.ControlsMarkedAsSplit(resultEntity.RaceClass);
            var passedCountDictionary = new Dictionary<int, int>();
            foreach (var control in controlsMarkedAsSplitTime)
            {
                var codeNumber = control.Id;
                if(!passedCountDictionary.TryGetValue(codeNumber, out int passedCount))
                {
                    passedCount = 1;
                }

                var splitTime = resultEntity.SplitTimes
                    .Where(x => x.Id.SplitTimeControl.TimingControl.Id == codeNumber
                    && x.Id.PassedCount == passedCount)
                    .FirstOrDefault();

                if (splitTime == null)
                {
                    splitTimes.Add(new SplitTime
                    {
                        Number = control.Id,
                        PassedCount = passedCount,
                    });
                }
                else
                {
                    var ordinal = 1 + resultEntity.RaceClass.Results
                        .Where(x => x.SplitTimes
                            .Any(y => y.Id.SplitTimeControl.RaceClassSplitTimeControls
                                .Any(z => z.Id.SplitTimeControl.TimingControl == control
                                    && y.Id.PassedCount == passedCount
                                    && y.SplitTime < splitTime.SplitTime))).Count();

                    splitTimes.Add(new SplitTime
                    {
                        Time = TimeSpan.FromSeconds(splitTime.SplitTime / 100),
                        Number = splitTime.Id.SplitTimeControl.TimingControl.Id,
                        PassedCount = splitTime.Id.PassedCount,
                        Ordinal = ordinal,
                    });
                }
                passedCountDictionary[codeNumber] = passedCount + 1;
            }
            return splitTimes;
        }
    }
}
