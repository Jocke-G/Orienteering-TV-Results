using Microsoft.Extensions.Options;
using OrienteeringTvResults.Configuration;
using OrienteeringTvResults.Mqtt;

namespace OrienteeringTvResults
{
    public class MqttHostedService
    {
        internal MqttHandler Handler { get; private set; }

        public MqttHostedService(IOptions<ApplicationConfiguration> conf)
        {

            Handler = new MqttHandler();
            Handler.Initialize(conf.Value.Mqtt).Wait();
        }
    }
}
