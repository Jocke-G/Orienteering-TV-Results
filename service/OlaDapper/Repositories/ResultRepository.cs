using Dapper;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaDapper.Entities;
using System;
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
organisations.shortName AS club,
results.startTime,
results.finishTime,
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

        public IList<ResultEntity> GetWithSplitTimes(int eventId, int eventRaceId, int eventClassId)
        {
            var sql = @"SELECT persons.firstName,
persons.familyName,
organisations.shortName AS club,
results.resultId,
results.runnerStatus,
results.startTime,
results.finishTime,
results.totalTime,
results.modifyDate,

#SplitTimes
splitTimes.splitTimeControlId,
splitTimes.passedTime,
splitTimes.splitTime,
splitTimes.passedCount

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

#SplitTimes
LEFT JOIN splitTimes
	ON splitTimes.resultRaceIndividualNumber = results.resultId

WHERE eventClasses.eventId = @eventId 
    AND raceClasses.eventRaceId = @eventRaceId
    AND eventClasses.eventClassId = @eventClassId";

            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
                eventClassId = eventClassId,
            };

            var splitOn = "splitTimeControlId";

            IList<ResultEntity> builtResultEntity = new List<ResultEntity>();
            var tmp = _connection.Query<ResultEntity, SplitTimeEntity, ResultEntity>(sql, (x, y) => BuildResultWithSplitTime(x, y, ref builtResultEntity), param, splitOn: splitOn);
            var distinct = tmp.Distinct();
            return builtResultEntity;
        }

        private ResultEntity BuildResultWithSplitTime(ResultEntity resultEntity, SplitTimeEntity splitTimeEntity, ref IList<ResultEntity> builtResultEntityList)
        {
            var currentResultEntity = builtResultEntityList.SingleOrDefault(x => x.ResultId == resultEntity.ResultId);
            if(currentResultEntity == null)
            {
                builtResultEntityList.Add(resultEntity);
                currentResultEntity = resultEntity;
            }

            if(splitTimeEntity != null)
                currentResultEntity.SplitTimes.Add(splitTimeEntity);

            return currentResultEntity;
        }

        public bool HasNewResults(int eventId, int eventRaceId, int eventClassId, DateTime lastCheckTime)
        {
            var sql = @"SELECT COUNT(*) AS Count
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

#SplitTimes
LEFT JOIN splitTimes
    ON splitTimes.resultRaceIndividualNumber = results.resultId

WHERE eventClasses.eventId = 3
    AND raceClasses.eventRaceId = 3
    AND eventClasses.eventClassId = 127
    AND(results.modifyDate > @lastCheckTime OR splitTimes.modifyDate > @lastCheckTime)";

            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
                eventClassId = eventClassId,
                lastCheckTime = lastCheckTime,
            };

            var count = _connection.QuerySingle(sql, param);
            bool any = count.Count > 0;
            return any;
        }

        internal IList<Result> GetFinish(int eventId, int eventRaceId)
        {
            throw new NotImplementedException();
        }

        public IList<ResultEntity> GetFinish(int eventRaceId, int eventId, int limit)
        {
            var sql = @"SELECT persons.firstName,
persons.familyName,
results.runnerStatus,
results.modifyDate,
organisations.shortName AS club,
eventclasses.shortName AS className,
results.finishTime,
results.startTime,
(unix_timestamp(results.finishTime)-unix_timestamp(results.startTime)) * 100 AS totalTime,
(CASE
    WHEN ISNULL(results.finishTime) OR runnerStatus='notValid' THEN NULL
    ELSE (SELECT COUNT(*) FROM results r2 WHERE results.raceClassId=r2.raceClassId AND r2.totalTime > 0 AND r2.totalTime < (results.totalTime) AND r2.runnerStatus NOT IN ('notValid'))+1
    END
) AS Ordinal
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
WHERE eventClasses.eventId = 3
    AND raceClasses.eventRaceId = 3
    AND finishTime > 0
   ORDER BY finishTime DESC LIMIT 0, 50;";

            var param = new
            {
                eventRaceId = eventRaceId,
                eventId = eventId,
            };

            return _connection
                .Query<ResultEntity>(sql, param)
                .ToList();
        }
    }
}
