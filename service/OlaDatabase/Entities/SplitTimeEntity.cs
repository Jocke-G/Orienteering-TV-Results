using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class SplitTimeEntity
    {
        public virtual SplitTimeEntityKey Id { get; set; }
        public virtual DateTime PassedTime { get; set; }
        public virtual int SplitTime { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }

        public SplitTimeEntity()
        {
        }

        public SplitTimeEntity(SplitTimeEntityKey id, DateTime passedTime, int splitTime)
            : this()
        {
            Id = id;
            PassedTime = passedTime;
            SplitTime = splitTime;
        }

        public SplitTimeEntity(ResultEntity result, SplitTimeControlEntity splitTimeControl, int passedCount, DateTime passedTime, int splitTime)
            : this(new SplitTimeEntityKey(result, splitTimeControl, passedCount), passedTime, splitTime)
        {
        }
    }

    public class SplitTimeEntityKey : IEquatable<SplitTimeEntityKey>
    {
        public virtual ResultEntity Result { get; set; }
        public virtual SplitTimeControlEntity SplitTimeControl { get; set; }
        public virtual int PassedCount { get; set; }

        public SplitTimeEntityKey()
        {
        }

        public SplitTimeEntityKey(ResultEntity result, SplitTimeControlEntity splitTimeControl, int passedCount)
            : this()
        {
            Result = result;
            SplitTimeControl = splitTimeControl;
            PassedCount = passedCount;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SplitTimeEntityKey);
        }

        public bool Equals(SplitTimeEntityKey other)
        {
            return other != null &&
                   EqualityComparer<ResultEntity>.Default.Equals(Result, other.Result) &&
                   EqualityComparer<SplitTimeControlEntity>.Default.Equals(SplitTimeControl, other.SplitTimeControl) &&
                   PassedCount == other.PassedCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Result, SplitTimeControl, PassedCount);
        }

        public static bool operator ==(SplitTimeEntityKey left, SplitTimeEntityKey right)
        {
            return EqualityComparer<SplitTimeEntityKey>.Default.Equals(left, right);
        }

        public static bool operator !=(SplitTimeEntityKey left, SplitTimeEntityKey right)
        {
            return !(left == right);
        }
    }
}
