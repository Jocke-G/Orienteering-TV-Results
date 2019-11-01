using OlaDapper.SimplifiedEntities;
using System.Collections.Generic;

namespace OrienteeringTvResults.OlaDapper.Entities
{
    public class CourseEntity
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public IList<SplitControlEntity> SplitControls { get; set; }

        public CourseEntity()
        {
            SplitControls = new List<SplitControlEntity>();
        }
    }
}
