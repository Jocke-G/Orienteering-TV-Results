using System;
using System.Collections.Generic;
using System.Linq;
using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;

namespace OlaDatabase.Repositories
{
    public class ResultRepository : IResultRepository
    {
        public IList<ResultEntity> GetBy(int eventRaceId, int eventClassId)
        {
            var session = SessionFactoryHelper.GetSession();
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

            var results = session.Query<ResultEntity>().Where(
                x => x.RaceClass.EventRace.EventRaceId == eventRaceId
                    && x.RaceClass.EventClass.EventClassId == eventClassId
                ).ToList();
            var orderedResults = results.OrderBy(item => preferences.IndexOf(item.RunnerStatus)).ThenBy(x => x.TotalTime).ToList();

            return orderedResults;
        }

        public bool HasNewResults(int eventId, int eventRaceId, int eventClassId, DateTime lastCheckTime)
        {
            var results = GetResults(eventId, eventRaceId, eventClassId);
            return results.Any(x => x.ModifyDate > lastCheckTime);
        }

        private IEnumerable<ResultEntity> GetResults(int eventId, int eventRaceId, int eventClassId)
        {
            var session = SessionFactoryHelper.GetSession();

            var results = session.Query<ResultEntity>()
                .Where(x => x.RaceClass.EventRace.EventRaceId == eventRaceId
                   && x.RaceClass.EventClass.EventClassId == eventClassId);

            return results;
        }
    }
}
