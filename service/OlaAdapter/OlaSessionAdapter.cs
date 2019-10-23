using OlaDatabase.Session;
using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.OlaAdapter
{
    public static class OlaSessionAdapter
    {
        public static void Initialize(OlaConfiguration conf)
        {
            var dbConf = conf.Database;
            Logger.LogInfo($"Using database '{dbConf.Database}' at '{dbConf.Server}' as user '{dbConf.Username}'. Will publish competition: '{conf.EventId}' stage: '{conf.EventRaceId}'");
            OlaDatabase.OlaDatabaseConfiguration olaDbConf = new OlaDatabase.OlaDatabaseConfiguration
            {
                Database = dbConf.Database,
                Username = dbConf.Username,
                Password = dbConf.Password,
                Server = dbConf.Server,
            };
            SessionFactoryHelper.Initialize(new MySqlSessionFactoryCreator(olaDbConf));
        }
    }
}
