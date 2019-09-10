using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace OlaDatabase.Repositories
{
    public class EventRepository: IEventRepository
    {
        public IList<EventEntity> GetAll()
        {
            var session = SessionFactoryHelper.GetSession();
            
            return session.Query<EventEntity>().ToList();
        }

        public EventEntity GetById(int id)
        {
            var session = SessionFactoryHelper.GetSession();
            var eventEntity = session.Query<EventEntity>().Where(x => x.EventId == id).First();
            return eventEntity;
        }
    }
}
