using Microsoft.Extensions.Options;
using OlaDatabase;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.OlaAdapter;

namespace OrienteeringTvResults
{
    public class ResultsAdapter
    {
        private ResultsProcessor _processor;

        public ResultsProcessor Processor { get { return _processor; } }

        public ResultsAdapter(IOptions<ApplicationConfiguration> conf)
        {
            SessionFactoryHelper.Initialize(new MySqlSessionFactoryCreator(conf.Value.Database));
           _processor = new ResultsProcessor();
        }
    }
}
