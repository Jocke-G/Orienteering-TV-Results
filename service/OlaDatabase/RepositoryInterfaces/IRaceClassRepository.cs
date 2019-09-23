using OlaDatabase.Entities;
using System;
using System.Linq;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IRaceClassRepository : IRepositoryWithEventIdAndEventRaceIdAndTypedId<RaceClassEntity, int>
    {
        RaceClassEntity GetByEventClassId(int eventId, int eventRaceId, int eventClassId);
        RaceClassEntity GetByShortName(int eventId, int eventRaceId, string shortName);
        IQueryable<RaceClassEntity> GetByEventIdAndEventRaceIdChangedSince(int eventId, int eventRaceId, DateTime lastModified);
    }
}
