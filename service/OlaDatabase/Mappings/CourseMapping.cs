using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class CourseMapping : ClassMap<CourseEntity>
    {
        CourseMapping()
        {
            Table("courses");

            Id(x => x.CourseId, "courseId").GeneratedBy.Increment();

            Map(x => x.Name, "name").Not.Nullable();
            Map(x => x.CourseLength, "courseLength").Nullable();
            Map(x => x.StartControl, "startControl").Nullable();
            Map(x => x.FinishControl, "finishControl").Nullable();
            References(x => x.EventRace, "eventRaceId").ForeignKey("Courses_FK00").Nullable();
            Map(x => x.ExternalId, "externalId").Nullable();
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("Courses_FK01").Nullable();

            HasMany(x => x.RaceClassCourses)
                .KeyColumn("courseId")
                .ForeignKeyConstraintName("RaceClassCourses_FK00")
                .Inverse();

            HasMany(x => x.RestartRaceClassCourses)
                .KeyColumn("restartCourseId")
                .ForeignKeyConstraintName("RaceClassCourses_FK03")
                .Inverse();

            HasMany(x => x.CoursesWayPointControls)
                .KeyColumn("courseId")
                .ForeignKeyConstraintName("CoursesWayPointControls_FK01")
                .Inverse();
        }
    }
}
