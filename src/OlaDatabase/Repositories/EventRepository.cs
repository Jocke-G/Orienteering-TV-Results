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
            var session = SessionFactoryHelper.OpenSession();
            return session.Query<EventEntity>().ToList();
        }

        public EventEntity GetById(int id)
        {
            var session = SessionFactoryHelper.OpenSession();
            var eventEntity = session.Query<EventEntity>().Where(x => x.EventId == id).First();
            return eventEntity;
        }
    }
}
