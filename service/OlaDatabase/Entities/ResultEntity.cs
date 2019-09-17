using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class ResultEntity
    {
        public virtual int ResultId { get; set; }
        public virtual string BibNumber { get; set; }
        public virtual int RaceStartNumber { get; set; }
        public virtual DateTime AllocatedStartTime { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime FinishTime { get; set; }
        public virtual int TotalTime { get; set; }
        public virtual int TimeAfter { get; set; }
        public virtual int Position { get; set; }
        public virtual string RunnerStatus { get; set; }
        public virtual int OverallTotalTime { get; set; }
        public virtual int OverallTimeAfter { get; set; }
        public virtual int OverallPosition { get; set; }
        public virtual string OverallRunnerStatus { get; set; }
        public virtual EntryEntity Entry { get; set; }
        public virtual int ElectronicPunchingCardId { get; set; }
        public virtual int IndividualCourseId { get; set; }
        public virtual int ForkedCourseId { get; set; }
        public virtual int ForkedCourseOrder { get; set; }
        public virtual RaceClassEntity RaceClass { get; set; }
        public virtual int RawDataFromElectronicPunchingCardsId { get; set; }
        public virtual int RelayPersonId { get; set; }
        public virtual int RelayPersonOrder { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual bool TakenCareOf { get; set; }
        public virtual string Comment { get; set; }
        public virtual IEnumerable<SplitTimeEntity> SplitTimes { get;  set; }
    }
}
