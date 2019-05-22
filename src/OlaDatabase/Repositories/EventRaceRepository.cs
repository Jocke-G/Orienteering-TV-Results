using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using System.Linq;

namespace OlaDatabase
{
    public class EventRaceRepository : IEventRaceRepository
    {
        public EventRaceEntity GetById(int eventRaceId)
        {
            var session = SessionFactoryHelper.OpenSession();
            var eventRace = session.Query<EventRaceEntity>().Where(x => x.EventRaceId == eventRaceId).First();
            return eventRace;
        }
    }
}
