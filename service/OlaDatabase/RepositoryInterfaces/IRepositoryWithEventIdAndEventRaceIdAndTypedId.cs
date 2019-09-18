using System.Linq;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IRepositoryWithEventIdAndEventRaceIdAndTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class 
    {
        IQueryable<T> GetByEventIdAndEventRaceId(int eventId, int eventRaceId);
        T GetById(int eventId, int eventRaceId, TId id);
    }
}
