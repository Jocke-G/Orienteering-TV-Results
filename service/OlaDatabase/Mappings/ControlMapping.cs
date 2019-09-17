using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class ControlMapping : ClassMap<ControlEntity>
    {
        ControlMapping()
        {
            Table("controls");

            Id(x => x.ControlId).Column("controlId").GeneratedBy.Increment();

            Map(x => x.Id, "ID").Index("ControlsID").Not.Nullable();
            Map(x => x.Name, "name").Nullable();
            Map(x => x.Location, "location").Nullable();
            Map(x => x.PathLength, "pathLength").Nullable();
            Map(x => x.TypeCode, "typeCode").Nullable();
            Map(x => x.ControlAreaName, "controlAreaName").Nullable();
            Map(x => x.Status, "status").Nullable();
            References(x => x.EventRace, "eventRaceId").ForeignKey("Controls_FK01").Nullable();
            Map(x => x.MaxFreeTime, "maxFreeTime").Default("0").Not.Nullable();
            Map(x => x.ControlAsFinish, "controlAsFinish").Not.Nullable();
            Map(x => x.XPos, "xPos").Nullable();
            Map(x => x.YPos, "yPos").Nullable();
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("Controls_FK02").Nullable();

            HasMany(x => x.SplitTimeControls)
                .KeyColumn("timingControl")
                .ForeignKeyConstraintName("SplitTimeControls_FK00")
                .LazyLoad()
                .Inverse();
        }
    }
}
