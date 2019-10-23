using OlaDatabase;
using OlaDatabase.Session;
using OrienteeringTvResults.Common.Configuration;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaAdapter.EagerFetchers;
using OrienteeringTvResults.OlaAdapter.Translators;
using System;
using System.Collections.Generic;

namespace OrienteeringTvResults.OlaAdapter
{
    public class OlaResultsProvider : IResultsProvider
    {
        private readonly int _competitionId;
        private readonly int _stageId;

        public OlaResultsProvider(OlaConfiguration databaseConfiguration)
        {
            _competitionId = databaseConfiguration.EventId;
            _stageId = databaseConfiguration.EventRaceId;
        }

        public IList<Competition> GetCompetitions()
        {
            using (var session = new SessionFactoryHelper())
            {
                var eventEntities = RepositoryContainer.EventRepository.GetAll();
                return CompetitionTranslator.ToCompetitions(eventEntities);
            }
        }

        public Competition GetCompetition(int competitionId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var eventEntity = RepositoryContainer.EventRepository.Get(competitionId);
                return CompetitionTranslator.ToCompetitionWithStages(eventEntity);
            }
        }

        public IList<CompetitionClass> GetClasses()
        {
            using (var session = new SessionFactoryHelper())
            {
                var raceClasses = RepositoryContainer.RaceClassRepository
                    .GetByEventIdAndEventRaceId(_competitionId, _stageId)
                    .EagerFetchSplitControls();
                return ClassTranslator.ToClasses(raceClasses);
            }
        }

        public IList<CompetitionClass> GetClassesChangedSince(DateTime since)
        {
            using (var session = new SessionFactoryHelper())
            {
                var raceClasses = RepositoryContainer.RaceClassRepository
                    .GetByEventIdAndEventRaceIdChangedSince(_competitionId, _stageId, since)
                    .EagerFetchSplitControls();

                return ClassTranslator.ToClasses(raceClasses);
            }
        }

        public CompetitionClass GetClass(int raceClassId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var raceClass = RepositoryContainer.RaceClassRepository
                    .GetById(_competitionId, _stageId, raceClassId);
                return ClassTranslator.ToClassWithResults(raceClass);
            }
        }

        public CompetitionClass GetClass(string shortName)
        {
            using (var session = new SessionFactoryHelper())
            {
                var raceClass = RepositoryContainer.RaceClassRepository
                    .GetByShortName(_competitionId, _stageId, shortName);
                return ClassTranslator.ToClassWithResults(raceClass);
            }
        }

        public bool ClassHasNewResults(int raceClassId, DateTime lastCheckTime)
        {
            using (var session = new SessionFactoryHelper())
            {
                return RepositoryContainer.ResultRepository.HasNewResults(_competitionId, _stageId, raceClassId, lastCheckTime);
            }
        }
    }
}
