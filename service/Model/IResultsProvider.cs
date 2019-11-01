using System;
using System.Collections.Generic;

namespace OrienteeringTvResults.Model
{
    public interface IResultsProvider
    {
        IList<CompetitionClass> GetClasses();
        bool ClassHasNewResults(int competitionClassId, DateTime lastCheckTime);
        CompetitionClass GetClass(int competitionCLassId);
        CompetitionClass GetClass(string shortName);
        IList<Result> Finish(int limit);
    }
}
