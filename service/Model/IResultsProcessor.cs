using System;
using System.Collections.Generic;

namespace OrienteeringTvResults.Model
{
    public interface IResultsProcessor
    {
        IList<Competition> GetCompetitions();
        Competition GetCompetition(int competitionId);
        IList<CompetitionClass> GetClasses();
        IList<CompetitionClass> GetClassesChangedSince(DateTime since);
        bool ClassHasNewResults(int competitionClassId, DateTime lastCheckTime);
        CompetitionClass GetClass(int competitionCLassId);
        CompetitionClass GetClass(string shortName);
    }
}
