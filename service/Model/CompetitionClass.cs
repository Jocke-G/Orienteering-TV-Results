using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrienteeringTvResults.Model
{
    public class CompetitionClass
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        [JsonIgnore]
        public bool NoTimePresentation { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<Result> Results { get; set; }
        public IList<SplitControl> SplitControls { get; set; }
    }
}
