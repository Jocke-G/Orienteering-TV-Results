using System.Collections.Generic;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IRepositoryWithEventIdAndEventRaceIdAndTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class 
    {
        IEnumerable<T> GetByEventIdAndEventRaceId(int eventId, int eventRaceId);
        T GetById(int eventId, int eventRaceId, TId id);
    }
}
