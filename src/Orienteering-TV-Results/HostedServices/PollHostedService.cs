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
            Logger.LogInfo("Constructing PollHostedService");
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
                    var classes = _results.Processor.GetClasses(3, 3);
                    foreach(var raceClass in classes)
                    {
                        Logger.LogInfo($"Fetching results for {raceClass.ShortName}");
                        var results = _results.Processor.GetClass(3, 3, raceClass.Id);
                        await MqttPublisher.PublishAsync(results);
                    }
                    var memoryUsed = GC.GetTotalMemory(false);

                    Logger.LogInfo($"Memory usage: { + memoryUsed / 1024 / 1024 } MB");
                    if(memoryUsed >= 70 * 1024 * 1024)
                    {
                        Logger.LogInfo("Forcing garbage collect");
                        GC.Collect();
                        Logger.LogInfo($"Memory usage after collect: { GC.GetTotalMemory(true) / 1024 / 1024 } MB");
                    }
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
