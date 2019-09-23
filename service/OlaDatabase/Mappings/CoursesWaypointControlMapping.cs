using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class CoursesWaypointControlMapping : ClassMap<CoursesWaypointControlEntity>
    {
        CoursesWaypointControlMapping()
        {
            Table("courseswaypointcontrols");

            CompositeId(x => x.Id)
                .KeyReference(x => x.Course, x => x.ForeignKey("CoursesWayPointControls_FK01"), "courseId")
                .KeyReference(x => x.Control, x => x.ForeignKey("CoursesWayPointControls_FK00"), "controlId")
                .KeyProperty(x => x.Ordered, "ordered");

            Map(x => x.DistanceFrom, "distanceFrom").Not.Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("CourseWayPointControls_FK02").Nullable();
        }
    }
}
