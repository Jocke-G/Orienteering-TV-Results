using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class RaceClassMapping : ClassMap<RaceClassEntity>
    {
        RaceClassMapping()
        {
            Table("raceclasses");

            Id(x => x.RaceClassId, "raceClassId").GeneratedBy.Increment();

            Map(x => x.RaceClassName).Column("raceClassName").Not.Nullable();
            Map(x => x.RaceClassLength).Column("raceClassLength").Nullable();
            //Map(x => x.EventClassId).Column("eventClassId").Nullable();
            References(x => x.EventClass).Column("eventClassId").ForeignKey("RaceClassesEventClassId").Not.Nullable();
            //Map(x => x.EventRaceId).Column("eventRaceId").Nullable();
            References(x => x.EventRace).Column("eventRaceId").ForeignKey("RaceClassesEventRaceId").Nullable();
            Map(x => x.FirstStartTime).Column("firstStartTime").Nullable();
            Map(x => x.StartInterval).Column("startInterval").Nullable();
            Map(x => x.StartUnit).Column("startUnit").Nullable();
            Map(x => x.AdministrationLineCode).Column("administrationLineCode").Nullable();
            Map(x => x.NoRankingRace).Column("noRankingRace").Not.Nullable();
            Map(x => x.EstimatedBestTime).Column("estimatedBestTime").Nullable();
            Map(x => x.EstimatedLastPrizeTime).Column("estimatedLastPrizeTime").Nullable();
            Map(x => x.NumberOfPrizes).Column("numberOfPrizes").Nullable();
            Map(x => x.PrizeCeremonyTime).Column("prizeCeremonyTime").Nullable();
            Map(x => x.PrizeGivingDone).Column("prizeGivingDone").Nullable();
            Map(x => x.StartNumberPrefix).Column("startNumberPrefix").Nullable();
            Map(x => x.StartNumberBase).Column("startNumberBase").Nullable();
            Map(x => x.StartNumberInherit).Column("startNumberInherit").Nullable();
            Map(x => x.FinishChute).Column("finishChute").Nullable();
            Map(x => x.BadgeGroupId).Column("badgeGroupId").Nullable();
            Map(x => x.RaceClassStatus).Column("raceClassStatus").Not.Nullable();
            Map(x => x.AllocationMethod).Column("allocationMethod").Not.Nullable();
            Map(x => x.ViewWhichOrganisation).Column("viewWhichOrganisation").Not.Nullable();
            Map(x => x.MaxAfterForChaseStart).Column("maxAfterForChaseStart").Nullable();
            Map(x => x.StartMethod).Column("startMethod").Not.Nullable();
            Map(x => x.EarlyStartLimit).Column("earlyStartLimit").Nullable();
            Map(x => x.LateStartLimit).Column("lateStartLimit").Nullable();
            Map(x => x.PunchingUnitType).Column("punchingUnitType").Nullable();
            Map(x => x.TimeResolution).Column("timeResolution").Not.Nullable();
            Map(x => x.RelayLeg).Column("relayLeg").Nullable();
            Map(x => x.MinRunners).Column("minRunners").Nullable();
            Map(x => x.MaxRunners).Column("maxRunners").Nullable();
            Map(x => x.RestartStopTime).Column("restartStopTime").Nullable();
            Map(x => x.RestartTime).Column("restartTime").Nullable();
            Map(x => x.RestartedTeamsAfter).Column("restartedTeamsAfter").Nullable();
            Map(x => x.NoOfEntries).Column("noOfEntries").Nullable();
            Map(x => x.NoOfStarts).Column("noOfStarts").Nullable();
            Map(x => x.StartsPerInterval).Column("startsPerInterval").Nullable();
            Map(x => x.MaxNumberInRaceClass).Column("maxNumberInRaceClass").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            Map(x => x.ModifiedBy).Column("modifiedBy").Nullable();

            HasMany(x => x.Results)
                .KeyColumn("raceClassId")
                .ForeignKeyConstraintName("Results_FK03")
                .Inverse();

            HasMany(x => x.RaceClassSplitTimeControls)
                .KeyColumn("raceClassId")
                .ForeignKeyConstraintName("RaceClassSplitTimeControl_FK00")
                .Inverse();
        }
    }
}
