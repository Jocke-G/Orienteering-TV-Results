using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class RaceClassSplitTimeControlEntity
    {
        public virtual RaceClassSplitTimeControlKey Id { get; set; }
        public virtual int Ordered { get; set; }
        public virtual int EstimatedBestTime { get; set; }
        public virtual int ModifiedBy { get; set; }
    }

    public class RaceClassSplitTimeControlKey : IEquatable<RaceClassSplitTimeControlKey>
    {
        public virtual RaceClassEntity RaceClass { get; set; }
        public virtual SplitTimeControlEntity SplitTimeControl { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as RaceClassSplitTimeControlKey);
        }

        public bool Equals(RaceClassSplitTimeControlKey other)
        {
            return other != null &&
                   EqualityComparer<RaceClassEntity>.Default.Equals(RaceClass, other.RaceClass) &&
                   EqualityComparer<SplitTimeControlEntity>.Default.Equals(SplitTimeControl, other.SplitTimeControl);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RaceClass, SplitTimeControl);
        }
    }
}
