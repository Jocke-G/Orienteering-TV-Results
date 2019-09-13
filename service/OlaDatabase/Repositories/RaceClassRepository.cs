using OlaDatabase.Entities;
using OlaDatabase.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace OlaDatabase
{
    public class RaceClassRepository : IRaceClassRepository
    {
        public IList<RaceClassEntity> GetByEventRaceId(int eventRaceId)
        {
            var session = SessionFactoryHelper.GetSession();
            var raceClasses = session.Query<RaceClassEntity>()
                .Where(
                    x => x.EventRace.EventRaceId == eventRaceId
                ).ToList();
            return raceClasses;

        }

        public RaceClassEntity GetByEventRaceIdAndId(int eventRaceId, int classId)
        {
            var session = SessionFactoryHelper.GetSession();
            var raceClass = session.Query<RaceClassEntity>()
                .Where(
                    x => x.EventRace.EventRaceId == eventRaceId
                    && x.EventClass.EventClassId == classId
                ).First();
            return raceClass;
        }

        public RaceClassEntity GetByShortName(int eventRaceId, string shortName)
        {
            var session = SessionFactoryHelper.GetSession();
            var raceClass = session.Query<RaceClassEntity>()
                .Where(
                    x => x.EventRace.EventRaceId == eventRaceId
                    && x.EventClass.ShortName == shortName
                ).First();
            return raceClass;
        }
    }
}
