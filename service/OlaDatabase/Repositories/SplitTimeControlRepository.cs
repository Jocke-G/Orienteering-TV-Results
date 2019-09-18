using System.Collections.Generic;
using System.Linq;
using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;

namespace OlaDatabase.Repositories
{
    public class SplitTimeControlRepository : RepositoryWithTypedId<SplitTimeControlEntity, int>, IRepositoryWithEventIdAndEventRaceIdAndTypedId<SplitTimeControlEntity, int>
    {
        public IQueryable<SplitTimeControlEntity> GetByEventIdAndEventRaceId(int eventId, int eventRaceId)
        {
            return Repository.Where(x => x.EventRace.Event.EventId == eventId && x.EventRace.EventRaceId == eventRaceId);
        }

        public SplitTimeControlEntity GetById(int eventId, int eventRaceId, int id)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .SingleOrDefault(x => x.SplitTimeControlId == id);
        }
    }
}
