using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using OlaDapper.Repositories;
using OlaDapper.Translators;
using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaDapper.Repositories;
using OrienteeringTvResults.OlaDapper.Translators;

namespace OrienteeringTvResults.OlaDapper
{
    public class OlaResultsProvider
        : IResultsProvider
    {
        private int _eventId;
        private int _eventRaceId;
        private OlaDatabaseConfiguration _databaseConfiguration;

        public OlaResultsProvider(OlaConfiguration olaConfiguration)
        {
            _eventId = olaConfiguration.EventId;
            _eventRaceId = olaConfiguration.EventRaceId;
            _databaseConfiguration = olaConfiguration.Database;
        }

        public IList<Competition> GetCompetitions()
        {
            throw new NotImplementedException();
        }

        public Competition GetCompetition(int competitionId)
        {
            throw new NotImplementedException();
        }

        public IList<CompetitionClass> GetClasses()
        {
            using(var connection = new MySqlConnection(_databaseConfiguration.GetConnectionString()))
            {
                var classRepository = new ClassRepository(connection);

                return classRepository
                    .GetByEventRaceIdAndEventId(_eventRaceId, _eventId)
                    .ToContracts();
            }
        }

        public IList<CompetitionClass> GetClassesChangedSince(DateTime since)
        {
            throw new NotImplementedException();
        }

        public CompetitionClass GetClass(int competitionClassId)
        {
            using (var connection = new MySqlConnection(_databaseConfiguration.GetConnectionString()))
            {
                var classRepository = new ClassRepository(connection);
                var resultRepository = new ResultRepository(connection);

                var classEntity = classRepository.GetByEventIdAndEventRaceIdAndEventClassId(_eventId, _eventRaceId, competitionClassId);
                var classContract = classEntity.ToContract();
                var results = resultRepository.Get(_eventRaceId, _eventId, competitionClassId);
                var resultContracts = results.ToContracts();
                classContract.Results = resultContracts;
                return classContract;
            }
        }

        public CompetitionClass GetClass(string shortName)
        {
            throw new NotImplementedException();
        }

        public bool ClassHasNewResults(int competitionClassId, DateTime lastCheckTime)
        {
            throw new NotImplementedException();
        }
    }
}
