using OlaDatabase.Entities;
using OlaDatabase.Repositories;
using OlaDatabase.RepositoryInterfaces;

namespace OlaDatabase
{
    public static class RepositoryContainer
    {
        public static IRepositoryWithTypedId<EventEntity, int> EventRepository { get; set; }
        public static IRepositoryWithTypedId<EventRaceEntity, int> EventRaceRepository { get; set; }
        public static IRaceClassRepository RaceClassRepository { get; set; }
        public static IResultRepository ResultRepository { get; set; }
        public static IRepositoryWithEventIdAndEventRaceIdAndTypedId<SplitTimeControlEntity, int> SplitTimeControlRepository { get; set; }

        static RepositoryContainer()
        {
            Initialize();
        }

        public static void Initialize()
        {
            EventRepository = new RepositoryWithTypedId<EventEntity, int>();
            EventRaceRepository = new RepositoryWithTypedId<EventRaceEntity, int>();
            RaceClassRepository = new RaceClassRepository();
            ResultRepository = new ResultRepository();
            SplitTimeControlRepository = new SplitTimeControlRepository();
        }
    }
}
