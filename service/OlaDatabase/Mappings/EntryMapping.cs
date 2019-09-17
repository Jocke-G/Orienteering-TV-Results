using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EntryMapping: ClassMap<EntryEntity>
    {
        public EntryMapping()
        {
            Table("entries");

            Id(x => x.EntryId, "entryId").GeneratedBy.Increment();

            Map(x => x.EventId).Column("eventId").Not.Nullable(); //Foreign Key - events - Entries_FK02
            Map(x => x.ExternalId).Column("externalId").Nullable();
            Map(x => x.EntryOrganisationId).Column("entryOrganisationId").Nullable(); //Foreign Key - organisations - Entries_FK00
            Map(x => x.AllocationControl).Column("allocationControl").Nullable();
            Map(x => x.AllocationEntryId).Column("allocationEntryId").Nullable();
            Map(x => x.AllocationOrganisationId).Column("allocationOrganisationId").Nullable();
            Map(x => x.AllocationPersonId).Column("allocationPersonId").Nullable();
            Map(x => x.SeedingGroup).Column("seedingGroup").Nullable();
            References(x => x.Person).Column("competitorId").ForeignKey("Entries_FK03").Nullable();
            Map(x => x.SportIdentCCardNumber).Column("sportIdentCCardNumber").Nullable();
            Map(x => x.EmitCCardNumber).Column("emitCCardNumber").Nullable();
            Map(x => x.AcceptedEventClassId).Column("acceptedEventClassId").Nullable();//Foreign Key - eventclasses - Entries_FK01
            Map(x => x.TeamName).Column("teamName").Nullable();
            Map(x => x.TeamLeader).Column("teamLeader").Nullable();
            Map(x => x.BibNumber).Column("bibNumber").Nullable();
            Map(x => x.Type).Column("type").Not.Nullable();
            Map(x => x.CreateDate).Column("createDate").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            Map(x => x.NoTotalResult).Column("noTotalResult").Not.Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();//Foreign Key - persons - Entries_FK04
            Map(x => x.ResultTag).Column("resultTag").Nullable();
        }
    }
}
