using System;
using System.Collections.Generic;

namespace OrienteeringTvResults.OlaDapper.Entities
{
    public class ResultEntity
    {
        public int ResultId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Club { get; set; }
        public string ClassName { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string RunnerStatus { get; set; }
        public int TotalTime { get; set; }
        public int? Ordinal { get; set; }
        public IList<SplitTimeEntity> SplitTimes { get; set; }

        public ResultEntity()
        {
            SplitTimes = new List<SplitTimeEntity>();
        }
    }
}
