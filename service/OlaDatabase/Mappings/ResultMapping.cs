using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class ResultMapping: ClassMap<ResultEntity>
    {
        public ResultMapping()
        {
            Table("results");

            Id(x => x.ResultId, "resultId").GeneratedBy.Increment();

            //Incomplete!!
            Map(x => x.TotalTime).Column("totalTime").Nullable();
            Map(x => x.RunnerStatus).Column("runnerStatus").Not.Nullable();
            References(x => x.Entry).Column("entryId").Nullable();
            References(x => x.RaceClass).Column("raceClassId");
        }
    }
}
