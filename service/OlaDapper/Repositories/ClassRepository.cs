using Dapper;
using OlaDapper.SimplifiedEntities;
using OrienteeringTvResults.OlaDapper.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OlaDapper.Repositories
{
    class ClassRepository
    {
        private IDbConnection _connection;

        public ClassRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IList<ClassEntity> GetByEventRaceIdAndEventId(int eventRaceId, int eventId)
        {
            string sql = @"
SELECT EventClasses.eventCLassId AS Id,
    EventClasses.shortName,
    EventClasses.noTimePresentation
FROM RaceClasses
LEFT JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId
WHERE RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventId = @eventId; ";
            var param = new {
                eventRaceId = eventRaceId,
                eventId = eventId,
            };
            return _connection
                .Query<ClassEntity>(sql, param)
                .ToList();
        }

        public ClassEntity GetByEventIdAndEventRaceIdAndEventClassId(int eventId, int eventRaceId, int eventClassId)
        {
            string sql = @"
SELECT EventClasses.eventCLassId AS Id,
    EventClasses.shortName,
    EventClasses.noTimePresentation
FROM RaceClasses
LEFT JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId
WHERE RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventId = @eventId
    AND EventClasses.eventClassId = @eventClassId";
            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
                eventClassId = eventClassId,
            };
            return _connection
                .QuerySingleOrDefault<ClassEntity>(sql, param);
        }

        public ClassEntity GetWithSplitControls(int eventId, int eventRaceId, int eventClassId)
        {
            string sql = @"
SELECT EventClasses.eventCLassId AS Id,
    EventClasses.shortName,
    EventClasses.noTimePresentation,
    Courses.courseId,
    Courses.name,
    Controls.controlId,
    SplitTimeControls.splitTimeControlId,
    SplitTimeControls.name
FROM RaceClasses
LEFT JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId

#Courses
LEFT JOIN RaceClassCourses
	ON RaceClassCourses.raceClassId = RaceClasses.raceClassId
LEFT JOIN Courses
	ON Courses.courseId = RaceClassCourses.courseId

#Controls
LEFT JOIN CoursesWayPointControls
	ON CoursesWayPointControls.courseId = Courses.courseId
LEFT JOIN Controls
	ON Controls.controlId = CoursesWayPointControls.controlId
LEFT JOIN SplitTimeControls
	ON SplitTimeControls.timingControl = Controls.controlId

WHERE RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventId = @eventId
    AND EventClasses.eventClassId = @eventClassId";
            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
                eventClassId = eventClassId,
            };
            var splitOn = "courseId,controlId";

            ClassEntity currentClassEntity = null;

            var classes = _connection
                .Query<ClassEntity, CourseEntity, SplitControlEntity, ClassEntity>(sql, (classEntity, courseEntity, controlEntity) =>
                {
                    return BuildWithSplitControls(classEntity, courseEntity, controlEntity, ref currentClassEntity);
                },
                param: param,
                splitOn: splitOn);

            return currentClassEntity;
        }

        public ClassEntity GetWithSplitControls(int eventId, int eventRaceId, string shortName)
        {
            string sql = @"
SELECT EventClasses.eventCLassId AS Id,
    EventClasses.shortName,
    EventClasses.noTimePresentation,
    Courses.courseId,
    Courses.name,
    Controls.controlId,
    SplitTimeControls.splitTimeControlId,
    SplitTimeControls.name
FROM RaceClasses
LEFT JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId

#Courses
LEFT JOIN RaceClassCourses
	ON RaceClassCourses.raceClassId = RaceClasses.raceClassId
LEFT JOIN Courses
	ON Courses.courseId = RaceClassCourses.courseId

#Controls
LEFT JOIN CoursesWayPointControls
	ON CoursesWayPointControls.courseId = Courses.courseId
LEFT JOIN Controls
	ON Controls.controlId = CoursesWayPointControls.controlId
LEFT JOIN SplitTimeControls
	ON SplitTimeControls.timingControl = Controls.controlId

WHERE RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventId = @eventId
    AND EventClasses.shortName = @shortName";
            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
                shortName = shortName,
            };
            var splitOn = "courseId,controlId";

            ClassEntity currentClassEntity = null;

            var classes = _connection
                .Query<ClassEntity, CourseEntity, SplitControlEntity, ClassEntity>(sql, (classEntity, courseEntity, controlEntity) =>
                {
                    return BuildWithSplitControls(classEntity, courseEntity, controlEntity, ref currentClassEntity);
                },
                param: param,
                splitOn: splitOn);

            return currentClassEntity;
        }

        private ClassEntity BuildWithSplitControls(ClassEntity classEntity, CourseEntity courseEntity, SplitControlEntity controlEntity, ref ClassEntity currentClassEntity)
        {
            if (currentClassEntity == null)
                currentClassEntity = classEntity;

            var currentCourse = currentClassEntity.Courses.SingleOrDefault(x => x.CourseId == courseEntity.CourseId);
            if (currentCourse == null && courseEntity != null)
            {
                currentCourse = courseEntity;
                currentClassEntity.Courses.Add(currentCourse);
            }

            if(controlEntity != null && controlEntity.Name != null)
            {
                currentCourse.SplitControls.Add(controlEntity);
            }

            return currentClassEntity;
        }
    }
}
