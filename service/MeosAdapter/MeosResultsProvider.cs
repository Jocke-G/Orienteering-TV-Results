using System;
using System.Collections.Generic;
using MeosDatabase;
using MeosDatabase.Session;
using OrienteeringTvResults.MeosAdapter.Translators;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.MeosAdapter
{
    public class MeosResultsProvider : IResultsProvider
    {
        private MeosConfiguration _conf;

        public MeosResultsProvider(MeosConfiguration conf)
        {
            _conf = conf;
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
            using (var session = new SessionFactoryHelper())
            {
                var classEntities = RepositoryContainer.ClassRepository.GetAll();
                return ClassTranslator.ToClasses(classEntities);
            }
        }

        public IList<CompetitionClass> GetClassesChangedSince(DateTime since)
        {
            throw new NotImplementedException();
        }

        public bool ClassHasNewResults(int competitionClassId, DateTime lastCheckTime)
        {
            throw new NotImplementedException();
        }

        public CompetitionClass GetClass(int competitionCLassId)
        {
            throw new NotImplementedException();
        }

        public CompetitionClass GetClass(string shortName)
        {
            throw new NotImplementedException();
        }
    }
}
