using System;

namespace OlaDatabase.Entities
{
    public class ResultEntity
    {
        public virtual int ResultId { get; set; }
        public virtual int TotalTime { get; set; }
        public virtual string RunnerStatus { get; set; }
        public virtual EntryEntity Entry { get; set; }
        public virtual RaceClassEntity RaceClass { get; set; }
        public virtual DateTime ModifyDate { get; set; } 
    }
}
