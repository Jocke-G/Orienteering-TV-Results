using Microsoft.Extensions.Options;
using OrienteeringTvResults.Configuration;
using OrienteeringTvResults.Mqtt;

namespace OrienteeringTvResults
{
    public class MqttHostedService
    {
        private MqttHandler _handler;

        public MqttHostedService(IOptions<ApplicationConfiguration> conf)
        {

            _handler = new MqttHandler();
            _handler.Initialize(conf.Value.Mqtt).Wait();
        }
    }
}
