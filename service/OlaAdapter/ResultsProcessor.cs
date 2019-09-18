using NHibernate.Linq;
using OlaDatabase;
using OlaDatabase.Session;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using OrienteeringTvResults.OlaAdapter.Translators;
using System;
using System.Collections.Generic;

namespace OrienteeringTvResults.OlaAdapter
{
    public class ResultsProcessor : IResultsProcessor
    {
        private readonly int _competitionId;
        private readonly int _stageId;

        public ResultsProcessor(DatabaseConfiguration databaseConfiguration)
        {
            _competitionId = databaseConfiguration.Competition;
            _stageId = databaseConfiguration.Stage;
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
                    .Fetch(x => x.EventClass)
                    .FetchMany(x => x.RaceClassSplitTimeControls)
                    .ThenFetch(x => x.Id)
                    .ThenFetch(x => x.SplitTimeControl);
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
