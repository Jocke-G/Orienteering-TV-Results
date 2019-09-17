using System.Collections.Generic;
using System.Linq;
using OlaDatabase.Entities;
using OlaDatabase.Repositories;
using OlaDatabase.RepositoryInterfaces;

namespace OlaDatabase
{
    internal class SplitTimeControlRepository : RepositoryWithTypedId<SplitTimeControlEntity, int>, ISplitTimeControlRepository
    {
        public IList<SplitTimeControlEntity> GetForEventRace(int competitionId, int stageId)
        {
            return Repository.Where(x => x.EventRace.EventRaceId == stageId).ToList();
        }
    }
}
