using OlaDatabase.Entities;
using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter.Translators
{
    static class ResultTranslator
    {
        internal static IList<Result> ToResultsWithoutTime(IList<ResultEntity> resultEntitiess)
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

            return results;
        }

        private static Result ToResultWithTimes(ResultEntity resultEntity)
        {
            return new Result
            {
                FirstName = resultEntity.Entry.Competitor.FirstName,
                LastName = resultEntity.Entry.Competitor.FamilyName,
                StartTime = resultEntity.StartTime,
                Status = resultEntity.RunnerStatus,
                Club = resultEntity.Entry.Competitor.DefaultOrganisation.Name,
                ModifyDate = resultEntity.ModifyDate,

                TotalTime = TimeSpan.FromSeconds(resultEntity.TotalTime / 100),
                SplitTimes = ToSplitTimes(resultEntity),
            };
        }

        private static Result ToResultWithoutTime(ResultEntity resultEntity)
        {
            return new Result
            {
                FirstName = resultEntity.Entry.Competitor.FirstName,
                LastName = resultEntity.Entry.Competitor.FamilyName,
                StartTime = resultEntity.StartTime,
                Status = resultEntity.RunnerStatus,
                Club = resultEntity.Entry.Competitor.DefaultOrganisation.Name,
                ModifyDate = resultEntity.ModifyDate,
            };
        }

        private static IList<SplitTime> ToSplitTimes(ResultEntity resultEntity)
        {
            var splitTimes = new List<SplitTime>();
            var raceClassSplitTimeControls = resultEntity.RaceClass.RaceClassSplitTimeControls;
            foreach (var raceClassSplitTimeControl in raceClassSplitTimeControls)
            {
                var splitTime = resultEntity.SplitTimes.Where(x => x.Id.SplitTimeControl == raceClassSplitTimeControl.Id.SplitTimeControl).FirstOrDefault();
                if (splitTime == null)
                {
                    splitTimes.Add(new SplitTime
                    {
                        Number = raceClassSplitTimeControl.Id.SplitTimeControl.TimingControl.Id
                    });
                }
                else
                {
                    splitTimes.Add(new SplitTime
                    {
                        Time = TimeSpan.FromSeconds(splitTime.SplitTime / 100),
                        Number = splitTime.Id.SplitTimeControl.TimingControl.Id,
                        PassedCount = splitTime.Id.PassedCount,
                        Ordinal = 1 + raceClassSplitTimeControl.Id.RaceClass.Results.Where(x => x.SplitTimes.Any(y => y.Id.SplitTimeControl.RaceClassSplitTimeControls.Any(z => z.Id.SplitTimeControl == raceClassSplitTimeControl.Id.SplitTimeControl && y.SplitTime < splitTime.SplitTime))).Count(),
                    });
                }
            }
            return splitTimes;
        }

        private static List<Result> OrderByStatusAndTime(List<Result> results)
        {
            var preferences = new List<string> {
                "passed",
                "finished",
                "notValid",
                "finishedTimeOk",
                "finishedPunchOk",
                "disqualified",
                "movedUp",
                "walkOver",
                "started",
                "notActivated",
                "notParticipating",
                "notStarted"
            };

            return results.OrderBy(item => preferences.IndexOf(item.Status)).ThenBy(x => x.TotalTime).ToList();
        }

        private static List<Result> AddOrdinal(List<Result> results)
        {
            var ordinal = 1;
            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                if (result.Status != "passed" && result.Status != "finished")
                    break;

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
