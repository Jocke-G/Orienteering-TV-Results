using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.Model
{
    public enum ResultStatus
    {
        Unknown,
        Passed,
        Finished,
        NotFinishedYet,
        NotPassed,
        NotStarted,
    }

    public class Result
        : IComparable<Result>
    {
        private const int thisPrecedes = -1;
        private const int thisIsSamePosition = 0;
        private const int thisFollows = 1;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Club { get; set; }
        public string ClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultStatus Status { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int? Ordinal { get; set; }
        public DateTime ModifyDate { get; set; }
        public IList<SplitTime> SplitTimes { get; set; }

        public int CompareTo(Result other)
        {
            if ((Status == ResultStatus.Passed || Status == ResultStatus.NotFinishedYet || Status == ResultStatus.Finished)
                && (other.Status == ResultStatus.NotStarted || other.Status == ResultStatus.NotPassed))
            {
                return thisPrecedes;
            }
            else if ((other.Status == ResultStatus.Passed || other.Status == ResultStatus.NotFinishedYet || other.Status == ResultStatus.Finished)
                && (Status == ResultStatus.NotStarted || Status == ResultStatus.NotPassed)){
                return thisFollows;
            }
            else if (other.Status == ResultStatus.NotPassed && Status == ResultStatus.NotStarted)
            {
                return thisFollows;
            }
            else if (other.Status == ResultStatus.NotStarted && other.Status == ResultStatus.NotStarted)
            {
                return thisPrecedes;
            }

            if(TotalTime != null && other.TotalTime != null)
            {
                if (TotalTime > other.TotalTime)
                {
                    return thisFollows;
                }
                else if(TotalTime < other.TotalTime)
                {
                    return thisPrecedes;
                }
                else
                {
                    return thisIsSamePosition;
                }
            }

            if (SplitTimes == null)
                return thisIsSamePosition;

            var lastCommonSplit = GetLastCommonSplit(other);
            if(lastCommonSplit != null)
            {
                return GetLeastTime(SplitTimes[lastCommonSplit.Value].Time.Value, other.SplitTimes[lastCommonSplit.Value].Time.Value);
            }

            if(SplitTimes.Any(x => x.Time.HasValue))
            {
                return thisPrecedes;
            }
            else if (other.SplitTimes.Any(x => x.Time.HasValue))
            {
                return thisFollows;
            }

            return thisIsSamePosition;
        }

        private int GetLeastTime(TimeSpan current, TimeSpan other)
        {
            if (current > other)
            {
                return thisFollows;
            }
            else if (current < other)
            {
                return thisPrecedes;
            }
            else
            {
                return thisIsSamePosition;
            }
        }

        private int? GetLastCommonSplit(Result other)
        {
            for(int i = SplitTimes.Count -1; i >= 0; i--)
            {
                if(SplitTimes[i].Time != null && other.SplitTimes[i].Time != null)
                {
                    return i;
                }
            }

            return null;
        }
    }
}
