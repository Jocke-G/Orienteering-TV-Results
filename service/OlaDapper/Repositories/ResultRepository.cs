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
            var sql = @"SELECT Persons.firstName,
Persons.familyName,
Results.runnerStatus,
Results.modifyDate,
Organisations.shortName AS club,
Results.startTime,
Results.finishTime,
Results.totalTime
FROM Results
INNER JOIN Entries
    ON Entries.entryID = Results.EntryId
INNER JOIN Persons
    ON Entries.competitorId = Persons.personId
INNER JOIN Organisations
    ON Persons.defaultOrganisationId = Organisations.organisationId
INNER JOIN RaceClasses
    ON RaceClasses.raceClassId = Results.raceClassId
INNER JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId
WHERE EventClasses.eventId = @eventId 
    AND RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventClassId = @eventClassId";

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
            var sql = @"SELECT Persons.firstName,
Persons.familyName,
Organisations.shortName AS club,
Results.resultId,
Results.runnerStatus,
Results.startTime,
Results.finishTime,
Results.totalTime,
Results.modifyDate,

#SplitTimes
SplitTimes.splitTimeControlId,
SplitTimes.passedTime,
SplitTimes.splitTime,
SplitTimes.passedCount

FROM Results
INNER JOIN Entries
    ON Entries.entryID = Results.EntryId
INNER JOIN Persons
    ON Entries.competitorId = Persons.personId
INNER JOIN Organisations
    ON Persons.defaultOrganisationId = Organisations.organisationId
INNER JOIN RaceClasses
    ON RaceClasses.RaceClassId = Results.raceClassId
INNER JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId

#SplitTimes
LEFT JOIN SplitTimes
	ON SplitTimes.ResultRaceIndividualNumber = Results.resultId

WHERE EventClasses.eventId = @eventId 
    AND RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventClassId = @eventClassId";

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
FROM Results
INNER JOIN Entries
    ON Entries.entryID = Results.EntryId
INNER JOIN Persons
    ON Entries.competitorId = Persons.personId
INNER JOIN Organisations
    ON Persons.defaultOrganisationId = Organisations.organisationId
INNER JOIN RaceClasses
    ON RaceClasses.raceClassId = Results.raceClassId
INNER JOIN EventClasses
    ON EventClasses.eventClassId = RaceClasses.eventClassId

#SplitTimes
LEFT JOIN SplitTimes
    ON SplitTimes.ResultRaceIndividualNumber = Results.resultId

WHERE EventClasses.eventId = @eventId
    AND RaceClasses.eventRaceId = @eventRaceId
    AND EventClasses.eventClassId = @eventClassId
    AND(Results.modifyDate > @lastCheckTime OR SplitTimes.modifyDate > @lastCheckTime)";

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
            var sql = @"SELECT Persons.firstName,
Persons.familyName,
Results.runnerStatus,
Results.modifyDate,
Organisations.shortName AS club,
EventClasses.shortName AS className,
Results.finishTime,
Results.startTime,
(unix_timestamp(Results.finishTime)-unix_timestamp(Results.startTime)) * 100 AS totalTime,
(CASE
    WHEN ISNULL(Results.finishTime) OR runnerStatus='notValid' THEN NULL
    ELSE (SELECT COUNT(*) FROM Results r2 WHERE Results.raceClassId=r2.raceClassId AND r2.totalTime > 0 AND r2.totalTime < (Results.totalTime) AND r2.runnerStatus NOT IN ('notValid'))+1
    END
) AS Ordinal
FROM Results
INNER JOIN Entries
    ON Entries.entryID = Results.EntryId
INNER JOIN Persons
    ON Entries.competitorId = Persons.personId
INNER JOIN Organisations
	ON Persons.defaultOrganisationId = Organisations.organisationId
INNER JOIN RaceClasses
	ON RaceClasses.raceClassId = Results.raceClassId
INNER JOIN EventClasses
	ON EventClasses.eventClassId = RaceClasses.eventClassId
WHERE EventClasses.eventId = @eventId
    AND RaceClasses.eventRaceId = @eventRaceId
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
