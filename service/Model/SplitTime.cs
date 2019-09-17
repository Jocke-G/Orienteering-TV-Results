using System;

namespace OrienteeringTvResults.Model
{
    public class SplitTime
    {
        public TimeSpan Time { get; set; }
        public int Number { get; set; }
        public int PassedCount { get; set; }
        public int Ordinal { get; set; }
    }
}
