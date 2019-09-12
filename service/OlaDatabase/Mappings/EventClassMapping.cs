using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EventClassMapping: ClassMap<EventClassEntity>
    {
        public EventClassMapping()
        {
            Table("eventclasses");

            Id(x => x.EventClassId, "eventClassId").GeneratedBy.Increment();

            Map(x => x.EventId).Column("eventId").Not.Nullable();
            Map(x => x.ClassStatus).Column("classStatus").Not.Nullable();
            Map(x => x.CollectedTo).Column("collectedTo").Nullable();
            Map(x => x.DividedFrom).Column("dividedFrom").Nullable();
            Map(x => x.FinalFromClassId).Column("finalFromClassId").Nullable();
            Map(x => x.ExternalId).Column("externalId").Nullable();
            Map(x => x.Name).Column("name").Not.Nullable();
            Map(x => x.ShortName).Column("shortName").Not.Nullable();
            Map(x => x.LowAge).Column("lowAge").Nullable();
            Map(x => x.HighAge).Column("highAge").Nullable();
            Map(x => x.MinAverageAge).Column("minAverageAge").Nullable();
            Map(x => x.MaxAverageAge).Column("maxAverageAge").Nullable();
            Map(x => x.Sex).Column("sex").Not.Nullable();
            Map(x => x.NumberInTeam).Column("numberInTeam").Nullable();
            Map(x => x.TeamEntry).Column("teamEntry").Not.Nullable();
            Map(x => x.NumberOfLegs).Column("numberOfLegs").Nullable();
            Map(x => x.NumberOfVacancies).Column("numberOfVacancies").Nullable();
            Map(x => x.MaxNumberInClass).Column("maxNumberInClass").Nullable();
            Map(x => x.DivideClassMethod).Column("divideClassMethod").Nullable();
            Map(x => x.ActualForRanking).Column("actualForRanking").Not.Nullable();
            Map(x => x.NoTimePresentation).Column("noTimePresentation").Not.Nullable();
            Map(x => x.SubstituteClassId).Column("substituteClassId").Nullable();
            Map(x => x.NotQualifiedSubstitutionClassId).Column("notQualifiedSubstitutionClassId").Nullable();
            Map(x => x.ClassTypeId).Column("classTypeId").Not.Nullable();
            Map(x => x.NormalizedClass).Column("normalizedClass").Not.Nullable();
            Map(x => x.BadgeGroupId).Column("badgeGroupId").Nullable();
            Map(x => x.NumberOfPrizesTotal).Column("numberOfPrizesTotal").Nullable();
            Map(x => x.NoTotalResult).Column("noTotalResult").Not.Nullable();
            Map(x => x.AllowEntryInAdvance).Column("allowEntryInAdvance").Not.Nullable();
            Map(x => x.AllowEventRaceEntry).Column("allowEventRaceEntry").Nullable();
            Map(x => x.AllowCardReusage).Column("allowCardReusage").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();
            Map(x => x.BaseClassId).Column("baseClassId").Nullable();
            Map(x => x.Sequence).Column("sequence").Nullable();
            Map(x => x.NoOfStarts).Column("noOfStarts").Nullable();
            Map(x => x.NoOfEntries).Column("noOfEntries").Nullable();
        }
    }
}
