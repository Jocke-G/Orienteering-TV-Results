using OlaDatabase.Entities;
using System;
using System.Collections.Generic;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IResultRepository : IRepositoryWithEventIdAndEventRaceIdAndTypedId<ResultEntity, int>
    {
        IEnumerable<ResultEntity> GetByEventClassId(int eventId, int eventRaceId, int eventClassId);
        bool HasNewResults(int eventId, int eventRaceId, int raceClassId, DateTime lastCheckTime);
    }
}
