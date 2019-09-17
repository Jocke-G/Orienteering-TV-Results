using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class EventRaceEntity
    {
        public virtual int EventRaceId { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual string Name { get; set; }
        public virtual EventEntity Event { get; set; }
        public virtual DateTime RaceDate { get; set; }
        public virtual string RaceStatus { get; set; }
        public virtual string RaceLightCondition { get; set; }
        public virtual string RaceDistance { get; set; }
        public virtual int AdministrationTime { get; set; }
        public virtual long XPos { get; set; }
        public virtual long YPos { get; set; }
        public virtual string ModifyDate { get; set; }
        public virtual int ModifiedBy { get; set; }
        public virtual IEnumerable<RaceClassEntity> RaceClasses { get; set; }
        public virtual IEnumerable<ControlEntity> Controls { get; set; }
        public virtual IEnumerable<SplitTimeControlEntity> SplitTimeControls { get; set; }

        public EventRaceEntity()
        {
            RaceClasses = new List<RaceClassEntity>();
            Controls = new List<ControlEntity>();
            SplitTimeControls = new List<SplitTimeControlEntity>();
        }
    }
}
