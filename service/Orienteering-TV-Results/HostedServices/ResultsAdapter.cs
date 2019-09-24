using Microsoft.Extensions.Options;
using OrienteeringTvResults.Configuration;
using OrienteeringTvResults.MeosAdapter;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaAdapter;
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
                    OlaSessionAdapter.Initialize(olaConf);
                    Processor = new OlaResultsProvider(olaConf);
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
