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
SELECT eventClasses.eventCLassId AS Id,
    eventClasses.shortName,
    eventClasses.noTimePresentation
FROM raceClasses
LEFT JOIN eventClasses
    ON eventClasses.eventClassId = raceClasses.eventClassId
WHERE raceClasses.eventRaceId = @eventRaceId
    AND eventClasses.eventId = @eventId; ";
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
SELECT eventClasses.eventCLassId AS Id,
    eventClasses.shortName,
    eventClasses.noTimePresentation
FROM raceClasses
LEFT JOIN eventClasses
    ON eventClasses.eventClassId = raceClasses.eventClassId
WHERE raceClasses.eventRaceId = @eventRaceId
    AND eventClasses.eventId = @eventId
    AND eventClasses.eventClassId = @eventClassId";
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
SELECT eventClasses.eventCLassId AS Id,
    eventClasses.shortName,
    eventClasses.noTimePresentation,
    courses.courseId,
    courses.name,
    controls.controlId,
    splitTimeControls.splitTimeControlId,
    splitTimeControls.name
FROM raceClasses
LEFT JOIN eventClasses
    ON eventClasses.eventClassId = raceClasses.eventClassId

#Courses
LEFT JOIN raceClassCourses
	ON raceClassCourses.raceClassId = raceClasses.raceClassId
LEFT JOIN courses
	ON courses.courseId = raceClassCourses.courseId

#Controls
LEFT JOIN coursesWaypointControls
	ON coursesWaypointControls.courseId = courses.courseId
LEFT JOIN controls
	ON controls.controlId = coursesWaypointControls.controlId
LEFT JOIN splitTimeControls
	ON splitTimeControls.timingControl = controls.controlId

WHERE raceClasses.eventRaceId = @eventRaceId
    AND eventClasses.eventId = @eventId
    AND eventClasses.eventClassId = @eventClassId";
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
SELECT eventClasses.eventCLassId AS Id,
    eventClasses.shortName,
    eventClasses.noTimePresentation,
    courses.courseId,
    courses.name,
    controls.controlId,
    splitTimeControls.splitTimeControlId,
    splitTimeControls.name
FROM raceClasses
LEFT JOIN eventClasses
    ON eventClasses.eventClassId = raceClasses.eventClassId

#Courses
LEFT JOIN raceClassCourses
	ON raceClassCourses.raceClassId = raceClasses.raceClassId
LEFT JOIN courses
	ON courses.courseId = raceClassCourses.courseId

#Controls
LEFT JOIN coursesWaypointControls
	ON coursesWaypointControls.courseId = courses.courseId
LEFT JOIN controls
	ON controls.controlId = coursesWaypointControls.controlId
LEFT JOIN splitTimeControls
	ON splitTimeControls.timingControl = controls.controlId

WHERE raceClasses.eventRaceId = @eventRaceId
    AND eventClasses.eventId = @eventId
    AND eventClasses.shortName = @shortName";
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
            if (currentCourse == null)
            {
                currentCourse = courseEntity;
                currentClassEntity.Courses.Add(currentCourse);
            }

            if(controlEntity.Name != null)
            {
                currentCourse.SplitControls.Add(controlEntity);
            }

            return currentClassEntity;
        }
    }
}
