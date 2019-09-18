using OlaDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IResultRepository : IRepositoryWithEventIdAndEventRaceIdAndTypedId<ResultEntity, int>
    {
        IEnumerable<ResultEntity> GetByEventClassId(int eventId, int eventRaceId, int eventClassId);
        bool HasNewResults(int eventId, int eventRaceId, int raceClassId, DateTime lastCheckTime);
        IQueryable<ResultEntity> GetByRaceClassId(int competitionId, int stageId, int raceClassId);
    }
}
