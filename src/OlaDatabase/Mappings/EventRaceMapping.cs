using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EventRaceMapping: ClassMap<EventRaceEntity>
    {
        EventRaceMapping()
        {
            Table("eventraces");

            Id(x => x.EventRaceId, "eventRaceId").GeneratedBy.Increment();

            Map(x => x.ExternalId).Column("externalId").Nullable();
            Map(x => x.Name).Column("name").Nullable();
            //Map(x => x.EventId).Column("eventId").Not.Nullable();
            References(x => x.Event).Column("eventId").ForeignKey("EventRaces_FK00").Not.Nullable();
            Map(x => x.RaceDate).Column("raceDate").Nullable();
            Map(x => x.RaceStatus).Column("raceStatus").Not.Nullable();
            Map(x => x.RaceLightCondition).Column("raceLightCondition").Not.Nullable();
            Map(x => x.RaceDistance).Column("raceDistance").Not.Nullable();
            Map(x => x.AdministrationTime).Column("administrationTime").Nullable();
            Map(x => x.XPos).Column("xPos").Nullable();
            Map(x => x.YPos).Column("yPos").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();
            
            
            //HasMany(x => x.RaceClasses)
                //.KeyColumn("eventRaceId")
                //.ForeignKeyConstraintName("RaceClassesEventRaceId")
                //.Inverse();
        }
    }
}
