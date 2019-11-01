using System;

namespace OrienteeringTvResults.OlaDapper.Entities
{
    public class SplitTimeEntity
    {
        public int SplitTimeControlId { get; set; }
        public DateTime PassedTime { get; set; }
        public int SplitTime { get; set; }
        public int PassedCount { get; set; }
    }
}
