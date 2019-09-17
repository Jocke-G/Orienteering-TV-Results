using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EntryMapping : ClassMap<EntryEntity>
    {
        EntryMapping()
        {
            Table("entries");

            Id(x => x.EntryId, "entryId").GeneratedBy.Increment();

            References(x => x.Event).Column("eventId").ForeignKey("Entries_FK02").Not.Nullable();
            Map(x => x.ExternalId).Column("externalId").Nullable();
            References(x => x.EntryOrganisation).Column("entryOrganisationId").ForeignKey("Entries_FK00").Nullable();
            Map(x => x.AllocationControl).Column("allocationControl").Nullable();
            Map(x => x.AllocationEntryId).Column("allocationEntryId").Nullable();
            Map(x => x.AllocationOrganisationId).Column("allocationOrganisationId").Nullable();
            Map(x => x.AllocationPersonId).Column("allocationPersonId").Nullable();
            Map(x => x.SeedingGroup).Column("seedingGroup").Nullable();
            References(x => x.Competitor).Column("competitorId").ForeignKey("Entries_FK03").Nullable();
            Map(x => x.SportIdentCCardNumber).Column("sportIdentCCardNumber").Nullable();
            Map(x => x.EmitCCardNumber).Column("emitCCardNumber").Nullable();
            References(x => x.AcceptedEventClass).Column("acceptedEventClassId").ForeignKey("Entries_FK01").Nullable();
            Map(x => x.TeamName).Column("teamName").Nullable();
            Map(x => x.TeamLeader).Column("teamLeader").Nullable();
            Map(x => x.BibNumber).Column("bibNumber").Nullable();
            Map(x => x.Type).Column("type").Not.Nullable();
            Map(x => x.CreateDate).Column("createDate").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            Map(x => x.NoTotalResult).Column("noTotalResult").Not.Nullable();
            References(x => x.ModifiedBy).Column("modifiedBy").ForeignKey("Entries_FK04").Nullable();
            Map(x => x.ResultTag).Column("resultTag").Nullable();

            HasMany(x => x.Results)
                .KeyColumn("entryId")
                .ForeignKeyConstraintName("Results_FK02")
                .LazyLoad()
                .Inverse();
        }
    }
}
