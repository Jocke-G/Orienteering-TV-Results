using System;
using System.Collections.Generic;
using System.Linq;
using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;

namespace OlaDatabase.Repositories
{
    public class ResultRepository : RepositoryWithTypedId<ResultEntity, int>, IResultRepository
    {
        public IEnumerable<ResultEntity> GetByEventIdAndEventRaceId(int eventId, int eventRaceId)
        {
            return Repository.Where(x => x.RaceClass.EventRace.Event.EventId == eventId && x.RaceClass.EventRace.EventRaceId == eventRaceId);
        }

        public ResultEntity GetById(int eventId, int eventRaceId, int id)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .SingleOrDefault(x => x.ResultId == id);
        }

        public IEnumerable<ResultEntity> GetByEventClassId(int eventId, int eventRaceId, int eventClassId)
        {
            return GetByEventIdAndEventRaceId(eventId, eventRaceId)
                .Where(x => x.RaceClass.EventClass.EventClassId == eventClassId);
        }

        public bool HasNewResults(int eventId, int eventRaceId, int eventClassId, DateTime lastCheckTime)
        {
            return GetByEventClassId(eventId, eventRaceId, eventClassId)
                .Any(x => x.ModifyDate > lastCheckTime);
        }
    }
}
