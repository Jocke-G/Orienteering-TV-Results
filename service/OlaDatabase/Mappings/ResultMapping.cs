using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class ResultMapping : ClassMap<ResultEntity>
    {
        ResultMapping()
        {
            Table("results");

            Id(x => x.ResultId, "resultId").GeneratedBy.Increment();

            Map(x => x.BibNumber).Column("bibNumber").Nullable();
            Map(x => x.RaceStartNumber).Column("raceStartNumber").Nullable();
            Map(x => x.AllocatedStartTime).Column("allocatedStartTime").Nullable();
            Map(x => x.StartTime).Column("startTime").Nullable();
            Map(x => x.FinishTime).Column("finishTime").Nullable();
            Map(x => x.TotalTime).Column("totalTime").Nullable();
            Map(x => x.TimeAfter).Column("timeAfter").Nullable();
            Map(x => x.Position).Column("position").Nullable();
            Map(x => x.RunnerStatus).Column("runnerStatus").Not.Nullable();
            Map(x => x.OverallTotalTime).Column("overallTotalTime").Nullable();
            Map(x => x.OverallTimeAfter).Column("overallTimeAfter").Nullable();
            Map(x => x.OverallPosition).Column("overallPosition").Nullable();
            Map(x => x.OverallRunnerStatus).Column("overallRunnerStatus").Nullable();
            References(x => x.Entry).Column("entryId").ForeignKey("Results_FK02").Nullable();
            Map(x => x.ElectronicPunchingCardId).Column("electronicPunchingCardId").Nullable(); // Foreign Key - electronicpunchingcards - Results_FK01
            Map(x => x.IndividualCourseId).Column("individualCourseId").Nullable(); // Foreign Key - courses - Results_FK00
            Map(x => x.ForkedCourseId).Column("forkedCourseId").Nullable();
            Map(x => x.ForkedCourseOrder).Column("forkedCourseOrder").Nullable();
            References(x => x.RaceClass).Column("raceClassId").ForeignKey("Results_FK03").Nullable();
            Map(x => x.RawDataFromElectronicPunchingCardsId).Column("rawDataFromElectronicPunchingCardsId").Nullable();
            Map(x => x.RelayPersonId).Column("relayPersonId").Nullable();
            Map(x => x.RelayPersonOrder).Column("relayPersonOrder").Nullable();
            Map(x => x.CreateDate).Column("createDate").Nullable();
            Map(x => x.ModifyDate).Column("modifyDate").Nullable();
            References(x => x.ModifiedBy).Column("modifiedBy").ForeignKey("Results_FK05").Nullable();
            Map(x => x.TakenCareOf).Column("takenCareOf").Nullable();
            Map(x => x.Comment).Column("comment").Nullable();

            HasMany(x => x.SplitTimes)
                .KeyColumn("resultRaceIndividualNumber")
                .ForeignKeyConstraintName("SplitTimes_FK00")
                .Inverse();
        }
    }
}
