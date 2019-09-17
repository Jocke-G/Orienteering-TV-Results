using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class SplitTimeMapping : ClassMap<SplitTimeEntity>
    {
        SplitTimeMapping()
        {
            Table("splittimes");

            CompositeId(x => x.Id)
                .KeyReference(x => x.Result, "resultRaceIndividualNumber")
                .KeyReference(x => x.SplitTimeControl, "splitTimeControlId")
                .KeyProperty(x => x.PassedCount, "passedCount");

            Map(x => x.PassedTime, "passedTime").Not.Nullable();
            Map(x => x.SplitTime, "splitTime").Not.Nullable();
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("SplitTimes_FK02").Nullable();
        }
    }
}
