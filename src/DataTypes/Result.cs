using System;

namespace OrienteeringTvResults.Model
{
    public class Result
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
