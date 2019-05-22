using OlaDatabase.Entities;
using System.Collections.Generic;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IEventRepository
    {
        IList<EventEntity> GetAll();
        EventEntity GetById(int id);
    }
}
