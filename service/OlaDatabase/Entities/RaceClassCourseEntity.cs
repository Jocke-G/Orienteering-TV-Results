using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class RaceClassCourseEntity
    {
        public virtual RaceClassCourseKey Id { get; set; }
        public virtual CourseEntity RestartCourse { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }

        public RaceClassCourseEntity()
        {
        }

        public RaceClassCourseEntity(RaceClassCourseKey id)
            : this()
        {
            Id = id;
        }

        public RaceClassCourseEntity(RaceClassEntity raceClass, CourseEntity course)
            : this(new RaceClassCourseKey(raceClass, course))
        {
        }
    }

    public class RaceClassCourseKey : IEquatable<RaceClassCourseKey>
    {
        public virtual RaceClassEntity RaceClass { get; set; }
        public virtual CourseEntity Course { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as RaceClassCourseKey);
        }

        public RaceClassCourseKey()
        {
        }

        public RaceClassCourseKey(RaceClassEntity raceClass, CourseEntity course)
            : this()
        {
            RaceClass = raceClass;
            Course = course;
        }

        public bool Equals(RaceClassCourseKey other)
        {
            return other != null &&
                   EqualityComparer<RaceClassEntity>.Default.Equals(RaceClass, other.RaceClass) &&
                   EqualityComparer<CourseEntity>.Default.Equals(Course, other.Course);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RaceClass, Course);
        }

        public static bool operator ==(RaceClassCourseKey left, RaceClassCourseKey right)
        {
            return EqualityComparer<RaceClassCourseKey>.Default.Equals(left, right);
        }

        public static bool operator !=(RaceClassCourseKey left, RaceClassCourseKey right)
        {
            return !(left == right);
        }
    }
}
