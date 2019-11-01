using Dapper;
using OlaDapper.SimplifiedEntities;
using OrienteeringTvResults.OlaDapper.Entities;
using System;
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
SELECT eventclasses.eventCLassId AS Id,
    eventclasses.shortName,
    eventclasses.noTimePresentation
FROM raceclasses
LEFT JOIN eventclasses
    ON eventclasses.eventClassId = raceclasses.eventClassId
WHERE raceclasses.eventRaceId = @eventRaceId
    AND eventclasses.eventId = @eventId; ";
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
SELECT eventclasses.eventCLassId AS Id,
    eventclasses.shortName,
    eventclasses.noTimePresentation
FROM raceclasses
LEFT JOIN eventclasses
    ON eventclasses.eventClassId = raceclasses.eventClassId
WHERE raceclasses.eventRaceId = @eventRaceId
    AND eventclasses.eventId = @eventId
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
SELECT eventclasses.eventCLassId AS Id,
    eventclasses.shortName,
    eventclasses.noTimePresentation,
    courses.courseId,
    courses.name,
    controls.controlId,
    splittimecontrols.SplitTimeControlId,
    splittimecontrols.name
FROM raceclasses
LEFT JOIN eventclasses
    ON eventclasses.eventClassId = raceclasses.eventClassId

#Courses
LEFT JOIN raceclasscourses
	ON raceclasscourses.raceClassId = raceclasses.raceClassId
LEFT JOIN courses
	ON courses.courseId = raceclasscourses.courseId

#Controls
LEFT JOIN courseswaypointcontrols
	ON courseswaypointcontrols.courseId = courses.courseId
LEFT JOIN controls
	ON controls.controlId = courseswaypointcontrols.controlId
LEFT JOIN splittimecontrols
	ON splittimecontrols.timingControl = controls.controlId

WHERE raceclasses.eventRaceId = @eventRaceId
    AND eventclasses.eventId = @eventId
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
SELECT eventclasses.eventCLassId AS Id,
    eventclasses.shortName,
    eventclasses.noTimePresentation,
    courses.courseId,
    courses.name,
    controls.controlId,
    splittimecontrols.SplitTimeControlId,
    splittimecontrols.name
FROM raceclasses
LEFT JOIN eventclasses
    ON eventclasses.eventClassId = raceclasses.eventClassId

#Courses
LEFT JOIN raceclasscourses
	ON raceclasscourses.raceClassId = raceclasses.raceClassId
LEFT JOIN courses
	ON courses.courseId = raceclasscourses.courseId

#Controls
LEFT JOIN courseswaypointcontrols
	ON courseswaypointcontrols.courseId = courses.courseId
LEFT JOIN controls
	ON controls.controlId = courseswaypointcontrols.controlId
LEFT JOIN splittimecontrols
	ON splittimecontrols.timingControl = controls.controlId

WHERE raceclasses.eventRaceId = @eventRaceId
    AND eventclasses.eventId = @eventId
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
