using OlaDatabase;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter
{
    public class ResultsProcessor
    {
        public ResultsProcessor()
        {
        }

        public IList<Competition> GetCompetitions()
        {
            using (var session = SessionFactoryHelper.GetSession())
            {
                var eventEntities = RepositoryContainer.EventRepository.GetAll();
                var competitions = new List<Competition>();
                foreach (EventEntity eventEntity in eventEntities)
                {
                    competitions.Add(ToCompetition(eventEntity));
                }

                return competitions;
            }
        }

        public Competition GetCompetition(int id)
        {
            using (var session = SessionFactoryHelper.GetSession())
            {
                var eventEntity = RepositoryContainer.EventRepository.GetById(id);
                var competition = ToCompetition(eventEntity);
                var eventRaces = eventEntity.EventRaces.ToList();
                competition.CompetitionStages = ToStages(eventRaces);
                return competition;
            }
        }

        public CompetitionStage GetStage(int id, int stageId)
        {
            using (var session = SessionFactoryHelper.GetSession())
            {
                var eventRace = RepositoryContainer.EventRaceRepository.GetById(stageId);
                var stage = ToStage(eventRace);
                var raceClasses = RepositoryContainer.RaceClassRepository.GetByEventRaceId(eventRace.EventRaceId);
                stage.Classes = ToClasses(raceClasses);
                return stage;
            }
        }

        public CompetitionClass GetClass(int id, int stageId, int classId)
        {
            using (var session = SessionFactoryHelper.GetSession())
            {
                var raceClass = RepositoryContainer.RaceClassRepository.GetByEventRaceIdAndId(stageId, classId);
                var results = RepositoryContainer.ResultRepository.GetBy(stageId, classId);
                //raceClass.Results = results;
                var clazz = ToClass(raceClass);
                clazz.Results = ToResults(results);
                return clazz;
            }
        }

        private Competition ToCompetition(EventEntity eventEntity)
        {
            var competition = new Competition
            {
                Id = eventEntity.EventId,
                Name = eventEntity.Name
            };
            return competition;
        }

        private IList<CompetitionStage> ToStages(IList<EventRaceEntity> races)
        {
            var stages = new List<CompetitionStage>();
            foreach (var race in races)
            {
                stages.Add(ToStage(race));
            }
            return stages;
        }

        private CompetitionStage ToStage(EventRaceEntity eventRace)
        {
            var stage = new CompetitionStage
            {
                Id = eventRace.EventRaceId,
                Name = eventRace.Name
            };

            return stage;
        }

        private IList<CompetitionClass> ToClasses(IList<RaceClassEntity> raceClasses)
        {
            var classes = new List<CompetitionClass>();
            foreach (var raceClass in raceClasses)
            {
                classes.Add(ToClass(raceClass));
            }
            return classes;
        }

        private CompetitionClass ToClass(RaceClassEntity raceClass)
        {
            var clazz = new CompetitionClass
            {
                Id = raceClass.EventClass.EventClassId,
                ShortName = raceClass.EventClass?.ShortName
            };

            return clazz;
        }

        private IList<Result> ToResults(IList<ResultEntity> resultEntities)
        {
            var results = new List<Result>();
            foreach (var resultEntity in resultEntities)
            {
                results.Add(ToResult(resultEntity));
            }
            return results;
        }

        private Result ToResult(ResultEntity resultEntity)
        {
            return new Result
            {
                FirstName = resultEntity.Entry.Person.FirstName,
                LastName = resultEntity.Entry.Person.FamilyName,
                Status = resultEntity.RunnerStatus,
                TotalTime = TimeSpan.FromSeconds(resultEntity.TotalTime / 100)
            };
        }
    }
}
