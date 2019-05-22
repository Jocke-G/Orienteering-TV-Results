using OlaDatabase.Repositories;
using OlaDatabase.RepositoryInterfaces;

namespace OlaDatabase
{
    public static class RepositoryContainer
    {
        public static IEventRepository EventRepository { get; set; }
        public static IEventRaceRepository EventRaceRepository { get; set; }
        public static IRaceClassRepository RaceClassRepository { get; set; }
        public static IResultRepository ResultRepository { get; set; }

        static RepositoryContainer()
        {
            Initialize();
        }

        public static void Initialize()
        {
            EventRepository = new EventRepository();
            EventRaceRepository = new EventRaceRepository();
            RaceClassRepository = new RaceClassRepository();
            ResultRepository = new ResultRepository();
        }
    }
}
