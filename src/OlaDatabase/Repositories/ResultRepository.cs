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
                "notStarted"
            };

            var results = session.Query<ResultEntity>().Where(
                x => x.RaceClass.EventRace.EventRaceId == eventRaceId
                    && x.RaceClass.EventClass.EventClassId == eventClassId
                ).ToList();
            var orderedResults = results.OrderBy(item => preferences.IndexOf(item.RunnerStatus)).ThenBy(x => x.TotalTime).ToList();

            return orderedResults;
        }
    }
}
