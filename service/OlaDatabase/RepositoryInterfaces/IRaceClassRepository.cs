using OlaDatabase.Entities;

namespace OlaDatabase.RepositoryInterfaces
{
    public interface IRaceClassRepository : IRepositoryWithEventIdAndEventRaceIdAndTypedId<RaceClassEntity, int>
    {
        RaceClassEntity GetByEventClassId(int eventId, int eventRaceId, int eventClassId);
        RaceClassEntity GetByShortName(int eventId, int eventRaceId, string shortName);
    }
}
