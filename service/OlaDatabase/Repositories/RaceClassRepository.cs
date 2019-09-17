using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace OlaDatabase.Repositories
{
    public class RaceClassRepository : RepositoryWithTypedId<RaceClassEntity, int>, IRaceClassRepository
    {
        public IEnumerable<RaceClassEntity> GetByEventIdAndEventRaceId(int eventId, int eventRaceId)
        {
            return Repository.Where(x => x.EventRace.Event.EventId == eventId && x.EventRace.EventRaceId == eventRaceId);
        }

        public RaceClassEntity GetById(int eventId, int eventRaceId, int id)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .SingleOrDefault(x => x.RaceClassId == id);
        }

        public RaceClassEntity GetByEventClassId(int eventId, int eventRaceId, int eventClassId)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .SingleOrDefault(x => x.EventClass.EventClassId == eventClassId);
        }

        public RaceClassEntity GetByShortName(int eventId, int eventRaceId, string shortName)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .SingleOrDefault(x => x.EventClass.ShortName == shortName);
        }
    }
}
