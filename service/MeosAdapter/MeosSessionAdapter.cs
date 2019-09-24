using MeosDatabase.Session;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.MeosAdapter
{
    public static class MeosSessionAdapter
    {
        public static void Initialize(MeosConfiguration conf)
        {
            var dbConf = conf.Database;
            Logger.LogInfo($"Using database '{dbConf.Database}' at '{dbConf.Server}' as user '{dbConf.Username}'.");
            SessionFactoryHelper.Initialize(new MySqlSessionFactoryCreator(dbConf));
        }
    }
}
