using OlaDatabase.Session;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;

namespace OrienteeringTvResults.OlaAdapter
{
    public static class OlaSessionAdapter
    {
        public static void Initialize(OlaConfiguration conf)
        {
            var dbConf = conf.Database;
            Logger.LogInfo($"Using database '{dbConf.Database}' at '{dbConf.Server}' as user '{dbConf.Username}'. Will publish competition: '{conf.EventId}' stage: '{conf.EventRaceId}'");
            SessionFactoryHelper.Initialize(new MySqlSessionFactoryCreator(conf.Database));
        }
    }
}
