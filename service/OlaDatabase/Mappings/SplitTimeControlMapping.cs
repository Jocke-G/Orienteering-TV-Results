using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    public class SplitTimeControlMapping : ClassMap<SplitTimeControlEntity>
    {
        SplitTimeControlMapping()
        {
            Table("splittimecontrols");

            Id(x => x.SplitTimeControlId).Column("splitTimeControlId").GeneratedBy.Increment();

            Map(x => x.Name, "name").Nullable();
            References(x => x.TimingControl, "timingControl").ForeignKey("SplitTimeControls_FK00").Nullable();
            References(x => x.EventRace, "eventRaceId").ForeignKey("SplitTimeControls_FK01").Not.Nullable();
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            Map(x => x.ModifiedBy, "modifiedBy").Nullable(); // Foreign key - persons - SplitTimeControls_FK02

            HasMany(x => x.SplitTime)
                .KeyColumn("splitTimeControlId")
                .ForeignKeyConstraintName("SplitTimes_FK01")
                .Inverse();

            HasMany(x => x.RaceClassSplitTimeControls)
                .KeyColumn("splitTimeControlId")
                .ForeignKeyConstraintName("RaceClassSplitTimeControl_FK01")
                .Inverse();
        }
    }
}
