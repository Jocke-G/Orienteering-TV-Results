using Microsoft.Extensions.Options;
using OrienteeringTvResults.Configuration;
using OrienteeringTvResults.Model;
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
        private readonly ApplicationConfiguration _conf;
        private readonly ResultsAdapter _results;
        private Dictionary<int, DateTime> _lastCheckDictionary;

        public PollHostedService(IOptions<ApplicationConfiguration> conf, MqttHostedService mqtt, ResultsAdapter results)
        {
            Logger.LogInfo("Constructing PollHostedService");
            _conf = conf.Value;
            _results = results;
            _lastCheckDictionary = new Dictionary<int, DateTime>();
            mqtt.Handler.OnResendRequest += Reset;
        }

        public void Reset(object sender, EventArgs e)
        {
            _lastCheckDictionary.Clear();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Logger.LogInfo("Poll database...");
                    var competitionClasses = _results.Processor.GetClasses();
                    Logger.LogInfo($"Found {competitionClasses.Count} classes");
                    foreach (var competitionClass in competitionClasses)
                    {
                        if (!_lastCheckDictionary.TryGetValue(competitionClass.Id, out DateTime lastCheckTime))
                        {
                            lastCheckTime = DateTime.MinValue;
                        }
                        Logger.LogInfo($"Checking new results in '{competitionClass.ShortName}' ({competitionClass.Id}) since {lastCheckTime}");
                        bool resultsChanged = _results.Processor.ClassHasNewResults(competitionClass.Id, lastCheckTime);
                        if (resultsChanged)
                        {
                            Logger.LogInfo($"Fetching results for {competitionClass.ShortName}");
                            var results = _results.Processor.GetClass(competitionClass.Id);
                            await MqttPublisher.PublishAsync(results);
                            var lastModified = results.Results.Aggregate((r1, r2) => r1.ModifyDate > r2.ModifyDate ? r1 : r2).ModifyDate;
                            Logger.LogInfo($"Updating last check time for '{competitionClass.ShortName}' to {lastModified}");
                            _lastCheckDictionary[competitionClass.Id] = lastModified;
                        }
                        else
                        {
                            Logger.LogInfo("No new results found");
                        }
                    }
                    var memoryUsed = GC.GetTotalMemory(false);

                    Logger.LogInfo($"Memory usage: { +memoryUsed / 1024 / 1024 } MB");
                    if (memoryUsed >= 70 * 1024 * 1024)
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

                await Task.Delay(TimeSpan.FromMilliseconds(_conf.PollWaitTime), cancellationToken);
            }
        }
    }
}
