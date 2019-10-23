using Dapper;
using OrienteeringTvResults.OlaDapper.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OrienteeringTvResults.OlaDapper.Repositories
{
    public class ResultRepository
    {
        private IDbConnection _connection;

        public ResultRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IList<ResultEntity> Get(int eventId, int eventRaceId, int eventClassId)
        {
            var sql = @"SELECT persons.firstName,
persons.familyName,
results.runnerStatus,
results.modifyDate,
organisations.shortName club,
results.startTime,
#splitTimes_i.passedTime passedTime_i,
#splitTimes.passedTime,
#CAST(splitTimes_i.splitTime/100 AS UNSIGNED) AS splitTime_i
#splitTimes.splitTime,
results.totalTime
FROM results
INNER JOIN entries
    ON entries.entryID = results.EntryId
INNER JOIN persons
    ON entries.competitorId = persons.personId
INNER JOIN organisations
    ON persons.defaultOrganisationId = organisations.organisationId
INNER JOIN raceClasses
    ON raceClasses.raceClassId = results.raceClassId
INNER JOIN eventClasses
    ON eventClasses.eventClassId = raceClasses.eventClassId
WHERE eventClasses.eventId = @eventId 
    AND raceClasses.eventRaceId = @eventRaceId
    AND eventClasses.eventClassId = @eventClassId";
            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
                eventClassId = eventClassId,
            };
            return _connection
                .Query<ResultEntity>(sql, param)
                .ToList();

        }
    }
}
