using System.Collections.Generic;

namespace OrienteeringTvResults.Model
{
    public class CompetitionClass
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public IList<Result> Results { get; set; }
    }
}
