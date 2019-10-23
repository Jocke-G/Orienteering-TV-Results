using Microsoft.Extensions.Options;
using OrienteeringTvResults.Configuration;
using OrienteeringTvResults.MeosAdapter;
using OrienteeringTvResults.Model;
using System;

namespace OrienteeringTvResults
{
    public class ResultsAdapter
    {
        public IResultsProvider Processor { get; }

        public ResultsAdapter(IOptions<ApplicationConfiguration> conf)
        {
            switch (conf.Value.System)
            {
                case "ola":
                    var olaConf = conf.Value.Ola;
                    OlaAdapter.OlaSessionAdapter.Initialize(olaConf);
                    Processor = new OlaAdapter.OlaResultsProvider(olaConf);
                    break;
                case "ola-dapper":
                    var olaDapperConf = conf.Value.Ola;
                    Processor = new OlaDapper.OlaResultsProvider(olaDapperConf);
                    break;
                case "meos":
                    var meosConf = conf.Value.Meos;
                    MeosSessionAdapter.Initialize(meosConf);
                    Processor = new MeosResultsProvider(meosConf);
                    break;
                default:
                    throw new Exception($"Unknown System specified in configuration: '{conf.Value.System}'");
            }
        }
    }
}
