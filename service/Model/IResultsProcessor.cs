namespace OrienteeringTvResults.Model
{
    public interface IResultsProcessor
    {
        CompetitionClass GetClass(int competitionId, int stageId, int classId);
    }
}
