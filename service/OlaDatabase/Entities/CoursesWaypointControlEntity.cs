using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class CoursesWaypointControlEntity
    {
        public virtual CoursesWaypointControlKey Id { get; set; }
        public virtual int DistanceFrom { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }

        public CoursesWaypointControlEntity()
        {
        }

        public CoursesWaypointControlEntity(CoursesWaypointControlKey id, int distanceFrom = 0)
            : this()
        {
            Id = id;
            DistanceFrom = distanceFrom;
        }

        public CoursesWaypointControlEntity(CourseEntity course, ControlEntity control, int ordered, int distanceFrom = 0)
            : this(new CoursesWaypointControlKey(course, control, ordered), distanceFrom)
        {
        }
    }

    public class CoursesWaypointControlKey : IEquatable<CoursesWaypointControlKey>
    {
        public virtual CourseEntity Course { get; set; }
        public virtual ControlEntity Control { get; set; }
        public virtual int Ordered { get; set; }

        public CoursesWaypointControlKey()
        {
        }

        public CoursesWaypointControlKey(CourseEntity course, ControlEntity control, int ordered)
            : this()
        {
            Course = course;
            Control = control;
            Ordered = ordered;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CoursesWaypointControlKey);
        }

        public bool Equals(CoursesWaypointControlKey other)
        {
            return other != null &&
                   EqualityComparer<CourseEntity>.Default.Equals(Course, other.Course) &&
                   EqualityComparer<ControlEntity>.Default.Equals(Control, other.Control) &&
                   Ordered == other.Ordered;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Course, Control, Ordered);
        }

        public static bool operator ==(CoursesWaypointControlKey left, CoursesWaypointControlKey right)
        {
            return EqualityComparer<CoursesWaypointControlKey>.Default.Equals(left, right);
        }

        public static bool operator !=(CoursesWaypointControlKey left, CoursesWaypointControlKey right)
        {
            return !(left == right);
        }
    }
}
