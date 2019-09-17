using Microsoft.Extensions.Options;
using OlaDatabase.Session;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.OlaAdapter;

namespace OrienteeringTvResults
{
    public class ResultsAdapter
    {
        private IResultsProcessor _processor;

        public IResultsProcessor Processor { get { return _processor; } }

        public ResultsAdapter(IOptions<ApplicationConfiguration> conf)
        {
            var dbConf = conf.Value.Database;
            Logger.LogInfo($"Using database '{dbConf.Database}' at '{dbConf.Server}' as user '{dbConf.Username}'. Will publish competition: '{dbConf.Competition}' stage: '{dbConf.Stage}'");
            SessionFactoryHelper.Initialize(new MySqlSessionFactoryCreator(dbConf));
           _processor = new ResultsProcessor(conf.Value.Database);
        }
    }
}
