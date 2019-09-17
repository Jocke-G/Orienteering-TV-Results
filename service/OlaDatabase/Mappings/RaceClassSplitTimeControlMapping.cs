using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class RaceClassSplitTimeControlMapping : ClassMap<RaceClassSplitTimeControlEntity>
    {
        public RaceClassSplitTimeControlMapping()
        {
            Table("raceclasssplittimecontrols");

            CompositeId(x => x.Id)
                .KeyReference(x => x.RaceClass, "raceClassId")
                .KeyReference(x => x.SplitTimeControl, "splitTimeControlId");

            Map(x => x.Ordered).Column("ordered").Not.Nullable();
            Map(x => x.EstimatedBestTime).Column("estimatedBestTime").Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();
        }
    }
}
