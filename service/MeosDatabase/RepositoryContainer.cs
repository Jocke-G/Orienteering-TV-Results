using MeosDatabase.Entities;
using MeosDatabase.Repositories;
using MeosDatabase.RepositoryInterfaces;

namespace MeosDatabase
{
    public static class RepositoryContainer
    {
        static RepositoryContainer()
        {
            Initialize();
        }

        public static IRepositoryWithTypedId<ClassEntity, int> ClassRepository { get; set; }

        public static void Initialize()
        {
            ClassRepository = new RepositoryWithTypedId<ClassEntity, int>();
        }
    }
}
