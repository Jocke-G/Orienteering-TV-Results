using OlaDatabase.Entities;
namespace OlaDatabase.RepositoryInterfaces
{
    public interface IEventRaceRepository
    {
        EventRaceEntity GetById(int eventRaceId);
    }
}