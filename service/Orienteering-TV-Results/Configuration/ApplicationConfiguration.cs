using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model.Configuration;

namespace OrienteeringTvResults.Configuration
{
    public class ApplicationConfiguration
    {
        public MqttApplicationConfiguration Mqtt { get; set; }
        public OlaConfiguration Ola { get; set; }
        public double PollWaitTime { get; set; }
        public bool EnablePollService { get; set; }
    }
}
