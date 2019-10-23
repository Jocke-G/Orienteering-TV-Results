using System;

namespace OrienteeringTvResults.OlaDapper.Entities
{
    public class ResultEntity
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Club { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime StartTime { get; set; }
        public string RunnerStatus { get; set; }
        public int TotalTime { get; set; }
    }
}
