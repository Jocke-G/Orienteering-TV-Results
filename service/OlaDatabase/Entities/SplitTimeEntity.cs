using System;

namespace OlaDatabase.Entities
{
    public class SplitTimeEntity
    {
        public virtual SplitTimeEntityKey Id { get; set; }

        public virtual DateTime PassedTime { get; set; }
        public virtual int SplitTime { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual int ModifiedBy { get; set; }
    }

    public class SplitTimeEntityKey : IEquatable<SplitTimeEntityKey>
    {
        public virtual ResultEntity Result { get; set; }
        public virtual SplitTimeControlEntity SplitTimeControl { get; set; }
        public virtual int PassedCount { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as SplitTimeEntityKey);
        }

        public bool Equals(SplitTimeEntityKey other)
        {
            return other != null &&
                   Result.ResultId == other.Result.ResultId &&
                   SplitTimeControl.SplitTimeControlId == other.SplitTimeControl.SplitTimeControlId &&
                   PassedCount == other.PassedCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Result.ResultId, SplitTimeControl.SplitTimeControlId, PassedCount);
        }
    }
}
