using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class ControlEntity
    {
        public virtual int ControlId { get; set; }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
        public virtual string PathLength { get; set; }
        public virtual string TypeCode { get; set; }
        public virtual string ControlAreaName { get; set; }
        public virtual string Status { get; set; }
        public virtual EventRaceEntity EventRace { get; set; }
        public virtual int MaxFreeTime { get; set; }
        public virtual bool ControlAsFinish { get; set; }
        public virtual double XPos { get; set; }
        public virtual double YPos { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual IEnumerable<SplitTimeControlEntity> SplitTimeControls { get; set; }

        public ControlEntity()
        {
            SplitTimeControls = new List<SplitTimeControlEntity>();
        }
    }
}
