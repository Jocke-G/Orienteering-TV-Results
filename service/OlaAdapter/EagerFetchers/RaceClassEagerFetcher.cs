using OlaDatabase.Entities;
using System.Linq;

namespace OrienteeringTvResults.OlaAdapter.EagerFetchers
{
    static class RaceClassEagerFetcher
    {
        internal static IQueryable<RaceClassEntity> EagerFetchSplitControls(this IQueryable<RaceClassEntity> raceClasses)
        {
            return raceClasses
                .EagerlyFetch(x => x.EventClass)
                .EagerlyFetchMany(x => x.RaceClassCourses)
                .ThenEagerlyFetch(x => x.Id)
                .ThenEagerlyFetch(x => x.Course)
                .ThenEagerlyFetchMany(x => x.CoursesWayPointControls)
                .ThenEagerlyFetch(x => x.Id)
                .ThenEagerlyFetch(x => x.Control)
                .ThenEagerlyFetchMany(x => x.SplitTimeControls);
        }
    }
}
