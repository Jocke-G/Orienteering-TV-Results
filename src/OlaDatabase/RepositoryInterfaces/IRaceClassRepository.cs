using OlaDatabase.Entities;
using System.Collections.Generic;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IRaceClassRepository
    {
        IList<RaceClassEntity> GetByEventRaceId(int eventRaceId);
        RaceClassEntity GetByEventRaceIdAndId(int eventRaceId, int classId);
    }
}