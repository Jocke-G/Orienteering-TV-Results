﻿using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class EventEntity
    {
        public virtual int EventId { get ;set; }
        public virtual string Name { get; set; }
        public virtual string EventNumber { get; set; }
        public virtual int District { get; set; }
        public virtual string TextURL { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime FinishDate { get; set; }
        public virtual string PostalGiroAccount { get; set; }
        public virtual string EventForm { get; set; }
        public virtual int EventClassificationTypeId { get; set; }
        public virtual int DefaultBadgeGroup { get; set; }
        public virtual bool PunchingManual { get; set; }
        public virtual bool PunchingSportIdent { get; set; }
        public virtual bool PunchingEmit { get; set; }
        public virtual int DefaultRankingListId { get; set; }
        public virtual int EventStatusId { get; set; }
        public virtual string Comment { get; set; }
        public virtual string ClassTypeComment { get; set; }
        public virtual int ParentEventId { get; set; }
        public virtual string ModifyDate { get; set; }
        public virtual int ModifiedBy { get; set; }
        public virtual IList<EventRaceEntity> EventRaces { get; set; }

        public EventEntity()
        {
            EventRaces = new List<EventRaceEntity>();
        }
    }
}
