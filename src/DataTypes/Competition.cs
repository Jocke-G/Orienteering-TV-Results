using System.Collections.Generic;

namespace OrienteeringTvResults.Model
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CompetitionStage> CompetitionStages { get; set; }
    }
}
