using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class RaceClassCourseMapping : ClassMap<RaceClassCourseEntity>
    {
        RaceClassCourseMapping()
        {
            Table("raceclasscourses");

            CompositeId(x => x.Id)
                .KeyReference(x => x.RaceClass, x => x.ForeignKey("RaceClassCourses_FK01"), "raceClassId")
                .KeyReference(x => x.Course, x => x.ForeignKey("RaceClassCourses_FK00"), "courseId");

            References(x => x.RestartCourse, "restartCourseId").ForeignKey("RaceClassCourses_FK03").Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("RaceClassCourses_FK02").Nullable();
        }
    }
}
