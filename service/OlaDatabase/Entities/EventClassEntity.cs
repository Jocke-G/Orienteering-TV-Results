using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class EventClassEntity
    {
        public virtual int EventClassId { get; set; }
        public virtual EventEntity Event { get; set; }
        public virtual string ClassStatus { get; set; }
        public virtual int CollectedTo { get; set; }
        public virtual int DividedFrom { get; set; }
        public virtual int FinalFromClassId { get; set; }
        public virtual int ExternalId { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual int LowAge { get; set; }
        public virtual int HighAge { get; set; }
        public virtual decimal MinAverageAge { get; set; }
        public virtual decimal MaxAverageAge { get; set; }
        public virtual string Sex { get; set; }
        public virtual int NumberInTeam { get; set; }
        public virtual bool TeamEntry { get; set; }
        public virtual int NumberOfLegs { get; set; }
        public virtual int NumberOfVacancies { get; set; }
        public virtual int MaxNumberInClass { get; set; }
        public virtual string DivideClassMethod { get; set; }
        public virtual bool ActualForRanking { get; set; }
        public virtual bool NoTimePresentation { get; set; }
        public virtual int SubstituteClassId { get; set; }
        public virtual int NotQualifiedSubstitutionClassId { get; set; }
        public virtual int ClassTypeId { get; set; }
        public virtual bool NormalizedClass { get; set; }
        public virtual int BadgeGroupId { get; set; }
        public virtual int NumberOfPrizesTotal { get; set; }
        public virtual bool NoTotalResult { get; set; }
        public virtual bool AllowEntryInAdvance { get; set; }
        public virtual bool AllowEventRaceEntry { get; set; }
        public virtual bool AllowCardReusage { get; set; }
        public virtual string ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual int BaseClassId { get; set; }
        public virtual int Sequence { get; set; }
        public virtual int NoOfStarts { get; set; }
        public virtual int NoOfEntries { get; set; }
        public virtual IEnumerable<EntryEntity> AcceptedEntries { get; set; }
        public virtual IEnumerable<RaceClassEntity> RaceClasses { get;  set; }

        public EventClassEntity()
        {
            AcceptedEntries = new List<EntryEntity>();
            RaceClasses = new List<RaceClassEntity>();
        }
    }
}
