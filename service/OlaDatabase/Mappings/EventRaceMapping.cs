using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EventRaceMapping : ClassMap<EventRaceEntity>
    {
        EventRaceMapping()
        {
            Table("eventraces");

            Id(x => x.EventRaceId, "eventRaceId").GeneratedBy.Increment();

            Map(x => x.ExternalId).Column("externalId").Nullable();
            Map(x => x.Name).Column("name").Nullable();
            References(x => x.Event).Column("eventId").ForeignKey("EventRaces_FK00").Not.Nullable();
            Map(x => x.RaceDate).Column("raceDate").Nullable();
            Map(x => x.RaceStatus).Column("raceStatus").Not.Nullable();
            Map(x => x.RaceLightCondition, "raceLightCondition").Default("Day").Not.Nullable();
            Map(x => x.RaceDistance, "raceDistance").Default("Middle").Not.Nullable();
            Map(x => x.AdministrationTime).Column("administrationTime").Nullable();
            Map(x => x.XPos).Column("xPos").Nullable();
            Map(x => x.YPos).Column("yPos").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();

            HasMany(x => x.Controls)
                .KeyColumn("eventRaceId")
                .ForeignKeyConstraintName("Controls_FK01")
                .Inverse();

            HasMany(x => x.SplitTimeControls)
                .KeyColumn("eventRaceId")
                .ForeignKeyConstraintName("SplitTimeControls_FK01")
                .Inverse();

            HasMany(x => x.RaceClasses)
                .KeyColumn("eventRaceId")
                .ForeignKeyConstraintName("RaceClasses_FK02")
                .Inverse();

            HasMany(x => x.Courses)
                .KeyColumn("eventRaceId")
                .ForeignKeyConstraintName("Courses_FK00")
                .Inverse();
        }
    }
}
