using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class RaceClassSplitTimeControlMapping : ClassMap<RaceClassSplitTimeControlEntity>
    {
        RaceClassSplitTimeControlMapping()
        {
            Table("raceclasssplittimecontrols");

            CompositeId(x => x.Id)
                .KeyReference(x => x.RaceClass, x => x.ForeignKey("RaceClassSplitTimeControl_FK00"), "raceClassId")
                .KeyReference(x => x.SplitTimeControl, x => x.ForeignKey("RaceClassSplitTimeControl_FK01"), "splitTimeControlId");

            Map(x => x.Ordered).Column("ordered").Not.Nullable();
            Map(x => x.EstimatedBestTime).Column("estimatedBestTime").Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();
        }
    }
}
