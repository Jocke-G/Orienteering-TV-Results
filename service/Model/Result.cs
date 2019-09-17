using System;
using System.Collections.Generic;

namespace OrienteeringTvResults.Model
{
    public class Result
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Club { get; set; }
        public DateTime StartTime { get; set; }
        public string Status { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int? Ordinal { get; set; }
        public DateTime ModifyDate { get; set; }
        public IList<SplitTime> SplitTimes { get; set; }
    }
}
