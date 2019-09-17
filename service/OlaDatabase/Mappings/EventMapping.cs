using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EventMapping : ClassMap<EventEntity>
    {
        EventMapping()
        {
            Table("events");

            Id(x => x.EventId, "eventId").GeneratedBy.Increment();

            Map(x => x.Name).Column("name").Not.Nullable();
            Map(x => x.EventNumber).Column("eventNumber").Nullable();
            Map(x => x.District).Column("district").Nullable();
            Map(x => x.TextURL).Column("textURL").Nullable();
            Map(x => x.StartDate).Column("startDate").Nullable();
            Map(x => x.FinishDate).Column("finishDate").Nullable();
            Map(x => x.PostalGiroAccount).Column("postalGiroAccount").Nullable();
            Map(x => x.EventForm).Column("eventForm").Not.Nullable();
            Map(x => x.EventClassificationTypeId).Column("eventClassificationTypeId").Nullable(); // Foreign Key - eventclassificationtypes - Events_FK01
            Map(x => x.DefaultBadgeGroup).Column("defaultBadgeGroup").Nullable(); //Foreign Key - badgegroups - Events_FK00
            Map(x => x.PunchingManual).Column("punchingManual").Not.Nullable();
            Map(x => x.PunchingSportIdent).Column("punchingSportIdent").Not.Nullable();
            Map(x => x.PunchingEmit).Column("punchingEmit").Not.Nullable();
            Map(x => x.DefaultRankingListId).Column("defaultRankingListId").Nullable();
            Map(x => x.EventStatusId).Column("eventStatusId").Not.Nullable(); // Foreign Key - eventstatuses - Events_FK02
            Map(x => x.Comment).Column("comment").Nullable();
            Map(x => x.ClassTypeComment).Column("classTypeComment").Nullable();
            Map(x => x.ParentEventId).Column("parentEventId").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            References(x => x.ModifiedBy).Column("modifiedBy").ForeignKey("Events_FK03").Nullable();

            HasMany(x => x.EventRaces)
                .KeyColumn("eventId")
                .ForeignKeyConstraintName("EventRaces_FK00")
                .ExtraLazyLoad()
                .Inverse();

            HasMany(x => x.EventClasses)
                .KeyColumn("eventId")
                .ForeignKeyConstraintName("EventClasses_FK01")
                .ExtraLazyLoad()
                .Inverse();
            
            HasMany(x => x.Entries)
                .KeyColumn("eventId")
                .ForeignKeyConstraintName("Entries_FK02")
                .ExtraLazyLoad()
                .Inverse();
        }
    }
}
