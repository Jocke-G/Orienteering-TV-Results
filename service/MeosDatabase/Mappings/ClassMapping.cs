using FluentNHibernate.Mapping;
using MeosDatabase.Entities;

namespace MeosDatabase.Mappings
{
    class ClassMapping : ClassMap<ClassEntity>
    {
        ClassMapping()
        {
            Table("oclass");

            Id(x => x.Id, "Id").GeneratedBy.Increment();

            Map(x => x.Name, "Name").Not.Nullable();
            Map(x => x.Course, "Course").Not.Nullable();
            Map(x => x.MultiCourse, "MultiCourse").Not.Nullable();
            Map(x => x.LegMethod, "LegMethod").Not.Nullable();
            Map(x => x.ExtId, "ExtId").Not.Nullable();
            Map(x => x.LongName, "LongName").Not.Nullable();
            Map(x => x.LowAge, "LowAge").Not.Nullable();
            Map(x => x.HighAge, "HighAge").Not.Nullable();
            Map(x => x.HasPool, "HasPool").Not.Nullable();
            Map(x => x.AllowQuickEntry, "AllowQuickEntry").Not.Nullable();
            Map(x => x.ClassType, "ClassType").Not.Nullable();
            Map(x => x.Sex, "Sex").Not.Nullable();
            Map(x => x.StartName, "StartName").Not.Nullable();
            Map(x => x.StartBlock, "StartBlock").Not.Nullable();
            Map(x => x.NoTiming, "NoTiming").Not.Nullable();
            Map(x => x.FreeStart, "FreeStart").Not.Nullable();
            Map(x => x.IgnoreStart, "IgnoreStart").Not.Nullable();
            Map(x => x.FirstStart, "FirstStart").Not.Nullable();
            Map(x => x.StartInterval, "StartInterval").Not.Nullable();
            Map(x => x.Vacant, "Vacant").Not.Nullable();
            Map(x => x.Reserved, "Reserved").Not.Nullable();
            Map(x => x.ClassFee, "ClassFee").Not.Nullable();
            Map(x => x.HighClassFee, "HighClassFee").Not.Nullable();
            Map(x => x.ClassFeeRed, "ClassFeeRed").Not.Nullable();
            Map(x => x.HighClassFeeRed, "HighClassFeeRed").Not.Nullable();
            Map(x => x.SortIndex, "SortIndex").Not.Nullable();
            Map(x => x.MaxTime, "MaxTime").Not.Nullable();
            Map(x => x.Status, "Status").Not.Nullable();
            Map(x => x.DirectResult, "DirectResult").Not.Nullable();
            Map(x => x.Bib, "Bib").Not.Nullable();
            Map(x => x.BibMode, "BibMode").Not.Nullable();
            Map(x => x.Unordered, "Unordered").Not.Nullable();
            Map(x => x.Heat, "Heat").Not.Nullable();
            Map(x => x.Locked, "Locked").Not.Nullable();
            Map(x => x.Qualification, "Qualification").Not.Nullable();
            Map(x => x.NumberMaps, "NumberMaps").Not.Nullable();
            Map(x => x.Modified, "Modified").Not.Nullable();
            Map(x => x.Counter, "Counter").Not.Nullable();
            Map(x => x.Removed, "Removed").Not.Nullable();
        }
    }
}
