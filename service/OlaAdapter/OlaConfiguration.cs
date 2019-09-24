using OlaDatabase;

namespace OrienteeringTvResults.Model.Configuration
{
    public class OlaConfiguration
    {
        public OlaDatabaseConfiguration Database { get; set; }
        public int EventId { get; set; }
        public int EventRaceId { get; set; }
    }
}
