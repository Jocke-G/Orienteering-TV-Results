using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class RaceClassEntity
    {
        public virtual int RaceClassId { get; set; }
        public virtual string RaceClassName { get; set; }
        public virtual string RaceClassLength { get; set; }
        //public virtual int EventClassId { get; set; }
        public virtual EventClassEntity EventClass { get; set; }
        //public virtual int EventRaceId { get; set; }
        public virtual EventRaceEntity EventRace { get; set; }
        public virtual DateTime FirstStartTime { get; set; }
        public virtual int StartInterval { get; set; }
        public virtual string StartUnit { get; set; }
        public virtual string AdministrationLineCode { get; set; }
        public virtual bool NoRankingRace { get; set; }
        public virtual int EstimatedBestTime { get; set; }
        public virtual int EstimatedLastPrizeTime { get; set; }
        public virtual int NumberOfPrizes { get; set; }
        public virtual DateTime PrizeCeremonyTime { get; set; }
        public virtual bool PrizeGivingDone { get; set; }
        public virtual string StartNumberPrefix { get; set; }
        public virtual string StartNumberBase { get; set; }
        public virtual bool StartNumberInherit { get; set; }
        public virtual string FinishChute { get; set; }
        public virtual int BadgeGroupId { get; set; }
        public virtual string RaceClassStatus { get; set; }
        public virtual string AllocationMethod { get; set; }
        public virtual string ViewWhichOrganisation { get; set; }
        public virtual int MaxAfterForChaseStart { get; set; }
        public virtual string StartMethod { get; set; }
        public virtual int EarlyStartLimit { get; set; }
        public virtual int LateStartLimit { get; set; }
        public virtual string PunchingUnitType { get; set; }
        public virtual int TimeResolution { get; set; }
        public virtual int RelayLeg { get; set; }
        public virtual int MinRunners { get; set; }
        public virtual int MaxRunners { get; set; }
        public virtual DateTime RestartStopTime { get; set; }
        public virtual DateTime RestartTime { get; set; }
        public virtual bool RestartedTeamsAfter { get; set; }
        public virtual int NoOfEntries { get; set; }
        public virtual int NoOfStarts { get; set; }
        public virtual int StartsPerInterval { get; set; }
        public virtual int MaxNumberInRaceClass { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual int ModifiedBy { get; set; }
        public virtual IList<ResultEntity> Results { get; set; }
    }
}
