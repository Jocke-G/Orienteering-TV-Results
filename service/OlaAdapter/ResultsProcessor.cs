using OlaDatabase;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter
{
    public class ResultsProcessor: IResultsProcessor
    {
        public IList<Competition> GetCompetitions()
        {
            using (var session = new SessionFactoryHelper())
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
            using (var session = new SessionFactoryHelper())
            {
                var eventEntity = RepositoryContainer.EventRepository.GetById(id);
                var competition = ToCompetition(eventEntity);
                var eventRaces = eventEntity.EventRaces.ToList();
                competition.CompetitionStages = ToStages(eventRaces);
                return competition;
            }
        }

        public IList<CompetitionClass> GetClasses(int eventId, int eventRaceId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var olaClasses = RepositoryContainer.RaceClassRepository.GetByEventRaceId(eventRaceId);
                return ToClasses(olaClasses);
            }
        }

        public CompetitionStage GetStage(int id, int stageId)
        {
            using (var session = new SessionFactoryHelper())
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
            using (var session = new SessionFactoryHelper())
            {
                var raceClass = RepositoryContainer.RaceClassRepository.GetByEventRaceIdAndId(stageId, classId);
                var results = RepositoryContainer.ResultRepository.GetBy(stageId, classId);
                raceClass.Results = results;
                var clazz = ToClass(raceClass);
                clazz.Results = ToResults(results);
                if (clazz.NoTimePresentation)
                {
                    clazz.Results = RemoveTime(clazz.Results.ToList());
                }
                else
                {
                    clazz.Results = AddOrdinal(clazz.Results.ToList());
                }
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
                ShortName = raceClass.EventClass.ShortName,
                NoTimePresentation = raceClass.EventClass.NoTimePresentation,
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

        private IList<Result> AddOrdinal(List<Result> results)
        {

            var ordinal = 1;
            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                if (result.Status != "passed" && result.Status != "finished")
                    break;

                result.Ordinal = ordinal;

                if (results.LastIndexOf(result) != i && results[i + 1].TotalTime > result.TotalTime)
                {
                    ordinal++;
                }
                else if (i > 0 && results[i - 1].TotalTime != result.TotalTime)
                {
                    ordinal = i + 1;
                }
                result.Ordinal = ordinal;
            }

            return results;
        }

        private IList<Result> RemoveTime(List<Result> results)
        {
            results.ForEach(x =>
            {
                x.TotalTime = null;
            });
            return results.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }

        private Result ToResult(ResultEntity resultEntity)
        {
            return new Result
            {
                FirstName = resultEntity.Entry.Person.FirstName,
                LastName = resultEntity.Entry.Person.FamilyName,
                Status = resultEntity.RunnerStatus,
                TotalTime = TimeSpan.FromSeconds(resultEntity.TotalTime / 100),
                Club = resultEntity.Entry.Person.Organisation.Name,
            };
        }
    }
}
