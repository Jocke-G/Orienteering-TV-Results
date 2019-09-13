using System;

namespace OrienteeringTvResults.Model
{
    public class Result
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Club { get; set; }
        public string Status { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int? Ordinal { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
