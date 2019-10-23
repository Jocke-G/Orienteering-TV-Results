using Dapper;
using OlaDapper.Entities;
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
    }
}
