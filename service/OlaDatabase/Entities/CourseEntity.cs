using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class CourseEntity
    {
        public virtual int CourseId { get; set; }
        public virtual string Name { get; set; }
        public virtual int CourseLength { get; set; }
        public virtual int StartControl { get; set; }
        public virtual int FinishControl { get; set; }
        public virtual EventRaceEntity EventRace { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual IEnumerable<RaceClassCourseEntity> RaceClassCourses { get; set; }
        public virtual IEnumerable<RaceClassCourseEntity> RestartRaceClassCourses { get; set; }
        public virtual IEnumerable<CoursesWaypointControlEntity> CoursesWayPointControls { get; set; }

        public CourseEntity()
        {
            RaceClassCourses = new List<RaceClassCourseEntity>();
            RestartRaceClassCourses = new List<RaceClassCourseEntity>();
            CoursesWayPointControls = new List<CoursesWaypointControlEntity>();
        }

        public CourseEntity(EventRaceEntity eventRace, string name)
            : this()
        {
            EventRace = eventRace;
            Name = name;
        }
    }
}
