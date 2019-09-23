﻿using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class SplitTimeControlEntity
    {
        public virtual int SplitTimeControlId { get; set; }
        public virtual string Name { get; set; }
        public virtual ControlEntity TimingControl { get; set; }
        public virtual EventRaceEntity EventRace { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual IEnumerable<SplitTimeEntity> SplitTimes { get; set; }
        public virtual IEnumerable<RaceClassSplitTimeControlEntity> RaceClassSplitTimeControls { get; set; }

        public SplitTimeControlEntity()
        {
            SplitTimes = new List<SplitTimeEntity>();
            RaceClassSplitTimeControls = new List<RaceClassSplitTimeControlEntity>();
        }

        public SplitTimeControlEntity(EventRaceEntity eventRace)
            : this()
        {
            EventRace = eventRace;
        }
    }
}