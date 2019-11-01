using Microsoft.Extensions.Options;
using OrienteeringTvResults.Configuration;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults
{
    public class ResultsAdapter
    {
        public IResultsProvider Processor { get; }

        public ResultsAdapter(IOptions<ApplicationConfiguration> conf)
        {
            var olaDapperConf = conf.Value.Ola;
            Processor = new OlaDapper.OlaResultsProvider(olaDapperConf);
        }
    }
}
