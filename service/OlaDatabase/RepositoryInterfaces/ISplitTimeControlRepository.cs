using OlaDatabase.Entities;
using System.Collections.Generic;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface ISplitTimeControlRepository : IRepositoryWithTypedId<SplitTimeControlEntity, int>
    {
        IList<SplitTimeControlEntity> GetForEventRace(int competitionId, int stageId);
    }
}
