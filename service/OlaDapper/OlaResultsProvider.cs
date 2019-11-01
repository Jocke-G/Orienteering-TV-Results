using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using OlaDapper.SimplifiedEntities;
using OlaDapper.Repositories;
using OlaDapper.Translators;
using OrienteeringTvResults.Common.Calculators;
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

        public CompetitionClass GetClass(int competitionClassId)
        {
            using (var connection = new MySqlConnection(_databaseConfiguration.GetConnectionString()))
            {
                var classRepository = new ClassRepository(connection);
                var resultRepository = new ResultRepository(connection);

                var classEntity = classRepository.GetWithSplitControls(_eventId, _eventRaceId, competitionClassId);

                var classContract = classEntity.ToContract();
                classContract.Results = GetResults(classEntity, resultRepository);
                return classContract;
            }
        }

        private IList<Result> GetResults(ClassEntity classContract, ResultRepository resultRepository)
        {
            if (classContract.NoTimePresentation)
            {
                var results = resultRepository.Get(_eventRaceId, _eventId, classContract.Id);
                var resultContracts = results.ToContractsWithoutTimes();

                return resultContracts;
            }
            else
            {
                var results = resultRepository.GetWithSplitTimes(_eventRaceId, _eventId, classContract.Id);
                var resultContracts = results.ToContractsWithSplitTimes(classContract.Courses.Single());

                if (!classContract.NoTimePresentation)
                {
                    resultContracts = ResultOrderer.OrderByStatusAndTime(resultContracts);
                    resultContracts = OrdinalCalculator.AddOrdinal(resultContracts);
                    resultContracts = ResultOrderer.OrderBySplitTimes(resultContracts);
                }

                return resultContracts;
            }
        }

        public CompetitionClass GetClass(string shortName)
        {
            using (var connection = new MySqlConnection(_databaseConfiguration.GetConnectionString()))
            {
                var classRepository = new ClassRepository(connection);
                var resultRepository = new ResultRepository(connection);

                var classEntity = classRepository.GetWithSplitControls(_eventId, _eventRaceId, shortName);

                var classContract = classEntity.ToContract();
                classContract.Results = GetResults(classEntity, resultRepository);
                return classContract;
            }
        }

        public bool ClassHasNewResults(int competitionClassId, DateTime lastCheckTime)
        {
            using (var connection = new MySqlConnection(_databaseConfiguration.GetConnectionString()))
            {
                var resultRepository = new ResultRepository(connection);

                return  resultRepository.HasNewResults(_eventId, _eventRaceId, competitionClassId, lastCheckTime);
            }
        }

        public IList<Result> Finish(int limit)
        {
            using (var connection = new MySqlConnection(_databaseConfiguration.GetConnectionString()))
            {
                var resultRepository = new ResultRepository(connection);

                return resultRepository
                    .GetFinish(_eventId, _eventRaceId, limit)
                    .ToContracts();
            }
        }
    }
}
