using NHibernate.Linq;
using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using System;
using System.Linq;

namespace OlaDatabase.Repositories
{
    public class RaceClassRepository : RepositoryWithTypedId<RaceClassEntity, int>, IRaceClassRepository
    {
        public IQueryable<RaceClassEntity> GetByEventIdAndEventRaceId(int eventId, int eventRaceId)
        {
            return Repository
                .Where(x => x.EventRace.Event.EventId == eventId 
                && x.EventRace.EventRaceId == eventRaceId);
        }

        public IQueryable<RaceClassEntity> GetByEventIdAndEventRaceIdChangedSince(int eventId, int eventRaceId, DateTime lastModified)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .Where(x => x.ModifyDate > lastModified);
        }

        public RaceClassEntity GetById(int eventId, int eventRaceId, int id)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .Where(x => x.RaceClassId == id)
                .FetchMany(x => x.RaceClassSplitTimeControls)
                .ThenFetch(x => x.Id)
                .ThenFetch(x => x.SplitTimeControl)
                .SingleOrDefault();
            ;
        }

        public RaceClassEntity GetByEventClassId(int eventId, int eventRaceId, int eventClassId)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .SingleOrDefault(x => x.EventClass.EventClassId == eventClassId);
        }

        public RaceClassEntity GetByShortName(int eventId, int eventRaceId, string shortName)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .Where(x => x.EventClass.ShortName == shortName)
                .FetchMany(x => x.RaceClassSplitTimeControls)
                .ThenFetch(x => x.Id)
                .ThenFetch(x => x.SplitTimeControl)
                .SingleOrDefault();
        }
    }
}
