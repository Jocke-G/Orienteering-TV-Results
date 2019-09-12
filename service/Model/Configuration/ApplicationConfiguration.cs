namespace OrienteeringTvResults.Model.Configuration
{
    public class ApplicationConfiguration
    {
        public MqttApplicationConfiguration Mqtt { get; set; }
        public DatabaseConfiguration Database { get; set; }
    }
}
