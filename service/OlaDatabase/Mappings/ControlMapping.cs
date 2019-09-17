using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    public class ControlMapping : ClassMap<ControlEntity>
    {
        public ControlMapping()
        {
            Table("controls");

            Id(x => x.ControlId).Column("controlId").GeneratedBy.Increment();

            Map(x => x.Id, "ID").Not.Nullable();
            Map(x => x.Name, "name").Nullable();
            Map(x => x.Location, "location").Nullable();
            Map(x => x.PathLength, "pathLength").Nullable();
            Map(x => x.TypeCode, "typeCode").Nullable();
            Map(x => x.ControlAreaName, "controlAreaName").Nullable();
            Map(x => x.Status, "status").Nullable();
            References(x => x.EventRace, "eventRaceId").ForeignKey("Controls_FK01").Nullable();
            Map(x => x.MaxFreeTime, "maxFreeTime").Nullable();
            Map(x => x.ControlAsFinish, "controlAsFinish").Nullable();
            Map(x => x.XPos, "xPos").Nullable();
            Map(x => x.YPos, "yPos").Nullable();
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            Map(x => x.ModifiedBy, "modifiedBy").Nullable(); // Foreign Key - Persons - Controls_FK02

            HasMany(x => x.SplitTimeControls)
                .KeyColumn("timingControl")
                .ForeignKeyConstraintName("SplitTimeControls_FK00")
                .Inverse();
        }
    }
}
