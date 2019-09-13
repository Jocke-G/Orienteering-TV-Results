using Microsoft.Extensions.Options;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrienteeringTvResults
{
    internal class PollHostedService : HostedServiceBase
    {
        private readonly DatabaseConfiguration _conf;
        private readonly ResultsAdapter _results;

        public PollHostedService(IOptions<ApplicationConfiguration> conf, MqttHostedService mqtt, ResultsAdapter results)
        {
            Logger.LogInfo("Constructing PollHostedService");
            _conf = conf.Value.Database;
            _results = results;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var lastCheckDictionary = new Dictionary<int, DateTime>();
            var eventId = _conf.Competition;
            var eventRaceId = _conf.Stage;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Logger.LogInfo("Poll database...");
                    var classes = _results.Processor.GetClasses(eventId, eventRaceId);
                    Logger.LogInfo($"Found {classes.Count} classes");
                    foreach(var raceClass in classes)
                    {
                        if (!lastCheckDictionary.TryGetValue(raceClass.Id, out DateTime lastCheckTime))
                        {
                            lastCheckTime = DateTime.MinValue;
                        }
                        Logger.LogInfo($"Checking new results in '{raceClass.ShortName}' ({raceClass.Id}) since {lastCheckTime}");
                        bool resultsChanged = _results.Processor.ClassHasNewResults(eventId, eventRaceId, raceClass.Id, lastCheckTime);
                        if (resultsChanged)
                        {
                            Logger.LogInfo($"Fetching results for {raceClass.ShortName}");
                            var results = _results.Processor.GetClass(3, 3, raceClass.Id);
                            await MqttPublisher.PublishAsync(results);
                            var lastModified = results.Results.Aggregate((r1, r2) => r1.ModifyDate > r2.ModifyDate ? r1 : r2).ModifyDate;
                            Logger.LogInfo($"Updating last check time for '{raceClass.ShortName}' to {lastModified}");
                            lastCheckDictionary[raceClass.Id] = lastModified;
                        }
                        else
                        {
                            Logger.LogInfo("No new results found");
                        }
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
