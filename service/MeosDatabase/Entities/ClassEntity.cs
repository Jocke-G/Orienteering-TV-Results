using System;

namespace MeosDatabase.Entities
{
    public class ClassEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual int Course { get; set; }
        public virtual string MultiCourse { get; set; }
        public virtual string LegMethod { get; set; } = string.Empty;
        public virtual long ExtId { get; set; }
        public virtual string LongName { get; set; } = string.Empty;
        public virtual int LowAge { get; set; }
        public virtual int HighAge { get; set; }
        public virtual int HasPool { get; set; }
        public virtual int AllowQuickEntry { get; set; }
        public virtual string ClassType { get; set; } = string.Empty;
        public virtual string Sex { get; set; } = string.Empty;
        public virtual string StartName { get; set; } = string.Empty;
        public virtual int StartBlock { get; set; }
        public virtual int NoTiming { get; set; }
        public virtual int FreeStart { get; set; }
        public virtual int IgnoreStart { get; set; }
        public virtual int FirstStart { get; set; }
        public virtual int StartInterval { get; set; }
        public virtual int Vacant { get; set; }
        public virtual int Reserved { get; set; }
        public virtual int ClassFee { get; set; }
        public virtual int HighClassFee { get; set; }
        public virtual int ClassFeeRed { get; set; }
        public virtual int HighClassFeeRed { get; set; }
        public virtual int SortIndex { get; set; }
        public virtual int MaxTime { get; set; }
        public virtual string Status { get; set; } = string.Empty;
        public virtual int DirectResult { get; set; }
        public virtual string Bib { get; set; } = string.Empty;
        public virtual string BibMode { get; set; } = string.Empty;
        public virtual int Unordered { get; set; }
        public virtual int Heat { get; set; }
        public virtual int Locked { get; set; }
        public virtual string Qualification { get; set; }
        public virtual int NumberMaps { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual int Counter { get; set; }
        public virtual bool Removed { get; set; }

        public ClassEntity()
        {
        }

        public ClassEntity(string multiCourse, string qualification)
            : this()
        {
            MultiCourse = multiCourse;
            Qualification = qualification;
        }
    }
}
