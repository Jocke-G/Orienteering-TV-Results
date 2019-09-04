using Microsoft.Extensions.Options;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.Mqtt;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrienteeringTvResults
{
    internal class PollHostedService : HostedServiceBase
    {
        private DatabaseConfiguration _conf;
        private ResultsAdapter _results;

        public PollHostedService(IOptions<DatabaseConfiguration> conf, MqttHostedService mqtt, ResultsAdapter results)
        {
            _conf = conf.Value;
            _results = results;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Logger.LogInfo("Poll database...");
                    var results = _results.Processor.GetClass(3, 3, 118);
                    await MqttPublisher.PublishAsync(results);
                }
                catch (Exception exception)
                {
                    Logger.LogError("Failed poll database", exception);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
