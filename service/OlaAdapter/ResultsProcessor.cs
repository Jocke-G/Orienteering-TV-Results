using OlaDatabase;
using OlaDatabase.Entities;
using OlaDatabase.Session;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter
{
    public class ResultsProcessor: IResultsProcessor
    {
        private int _stageId;
        private int _competitionId;

        public ResultsProcessor(DatabaseConfiguration databaseConfiguration)
        {
            _stageId = databaseConfiguration.Stage;
            _competitionId = databaseConfiguration.Competition;
        }

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

        public IList<SplitTimeControlEntity> GetSplitTimeControls(int competitionId, int stageId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var splitTimeControls = RepositoryContainer.SplitTimeControlRepository.GetByEventIdAndEventRaceId(competitionId, stageId);
                return splitTimeControls.ToList();
            }
        }

        public Competition GetCompetition(int id)
        {
            using (var session = new SessionFactoryHelper())
            {
                var eventEntity = RepositoryContainer.EventRepository.Get(id);
                var competition = ToCompetition(eventEntity);
                var eventRaces = eventEntity.EventRaces.ToList();
                competition.CompetitionStages = ToStages(eventRaces);
                return competition;
            }
        }

        public IList<CompetitionClass> GetClasses()
        {
            using (var session = new SessionFactoryHelper())
            {
                var olaClasses = RepositoryContainer.RaceClassRepository.GetByEventIdAndEventRaceId(_competitionId, _stageId);
                return ToClasses(olaClasses);
            }
        }

        public IList<CompetitionClass> GetClasses(int eventId, int eventRaceId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var olaClasses = RepositoryContainer.RaceClassRepository.GetByEventIdAndEventRaceId(eventId, eventRaceId);
                return ToClasses(olaClasses);
            }
        }

        public bool ClassHasNewResults(int eventId, int eventRaceId, int raceClassId, DateTime lastCheckTime)
        {
            using(var session = new SessionFactoryHelper())
            {
                bool newResults = RepositoryContainer.ResultRepository.HasNewResults(eventId, eventRaceId, raceClassId, lastCheckTime);
                return newResults;
            }

        }

        public CompetitionStage GetStage(int id, int stageId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var eventRace = RepositoryContainer.EventRaceRepository.Get(stageId);
                var stage = ToStage(eventRace);
                var raceClasses = RepositoryContainer.RaceClassRepository.GetByEventIdAndEventRaceId(id, stageId);
                stage.Classes = ToClasses(raceClasses);
                return stage;
            }
        }

        public CompetitionClass GetClass(string shortName)
        {
            using (var session = new SessionFactoryHelper())
            {
                var olaClass = RepositoryContainer.RaceClassRepository.GetByShortName(_competitionId, _stageId, shortName);
                var competitionClass = ToClass(olaClass);
                if (olaClass.EventClass.NoTimePresentation)
                {
                    competitionClass.Results = RemoveTime(ToResults(olaClass.Results).ToList());
                }
                else
                {
                    competitionClass.Results = AddOrdinal(ToResults(olaClass.Results).ToList());
                }
                return competitionClass;
            }
        }

        public CompetitionClass GetClass(int id, int stageId, int classId)
        {
            using (var session = new SessionFactoryHelper())
            {
                var olaClass = RepositoryContainer.RaceClassRepository.GetByEventClassId(id, stageId, classId);
                var competitionClass = ToClass(olaClass);
                if (olaClass.EventClass.NoTimePresentation)
                {
                    competitionClass.Results = RemoveTime(ToResults(olaClass.Results).ToList());
                }
                else
                {
                    competitionClass.Results = AddOrdinal(ToResults(olaClass.Results).ToList());
                }
                return competitionClass;
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

        private IList<CompetitionClass> ToClasses(IEnumerable<RaceClassEntity> raceClasses)
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
                SplitControls = ToSplitControls(raceClass)
            };

            return clazz;
        }

        private IList<SplitControl> ToSplitControls(RaceClassEntity raceClass)
        {
            var splitControls = new List<SplitControl>();
            foreach (var splitControl in raceClass.RaceClassSplitTimeControls.OrderBy(x => x.Ordered))
            {
                splitControls.Add(new SplitControl
                {
                    Name = splitControl.Id.SplitTimeControl.Name,
                });
            }

            return splitControls;
        }

        private IList<Result> ToResults(IEnumerable<ResultEntity> resultEntities)
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
            var preferences = new List<string> {
            "passed",
            "finished",
            "notValid",
            "finishedTimeOk",
            "finishedPunchOk",
            "disqualified",
            "movedUp",
            "walkOver",
            "started",
            "notActivated",
            "notParticipating",
            "notStarted"
        };

            results = results.OrderBy(item => preferences.IndexOf(item.Status)).ThenBy(x => x.TotalTime).ToList();

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
                FirstName = resultEntity.Entry.Competitor.FirstName,
                LastName = resultEntity.Entry.Competitor.FamilyName,
                StartTime = resultEntity.StartTime,
                Status = resultEntity.RunnerStatus,
                TotalTime = TimeSpan.FromSeconds(resultEntity.TotalTime / 100),
                Club = resultEntity.Entry.Competitor.DefaultOrganisation.Name,
                ModifyDate = resultEntity.ModifyDate,
                SplitTimes = ToSplitTimes(resultEntity),
            };
        }

        private IList<SplitTime> ToSplitTimes(ResultEntity resultEntity)
        {
            var splitTimes = new List<SplitTime>();
            var raceClassSplitTimeControls = resultEntity.RaceClass.RaceClassSplitTimeControls;
            foreach(var raceClassSplitTimeControl in raceClassSplitTimeControls)
            {
                var splitTime = resultEntity.SplitTimes.Where(x => x.Id.SplitTimeControl == raceClassSplitTimeControl.Id.SplitTimeControl).FirstOrDefault();
                if(splitTime == null)
                {
                    splitTimes.Add(new SplitTime
                    {
                        Number = raceClassSplitTimeControl.Id.SplitTimeControl.TimingControl.Id
                    });
                }
                else
                {
                    splitTimes.Add(new SplitTime {
                        Time = splitTime.SplitTime,
                        Number = splitTime.Id.SplitTimeControl.TimingControl.Id,
                        PassedCount = splitTime.Id.PassedCount,
                        Ordinal = 1 + raceClassSplitTimeControl.Id.RaceClass.Results.Where(x => x.SplitTimes.Any(y => y.Id.SplitTimeControl.RaceClassSplitTimeControls.Any(z => z.Id.SplitTimeControl == raceClassSplitTimeControl.Id.SplitTimeControl && y.SplitTime < splitTime.SplitTime))).Count(),
                    });
                }
            }
            return splitTimes;
        }
    }
}
