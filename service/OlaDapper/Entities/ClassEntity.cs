using OrienteeringTvResults.OlaDapper.Entities;
using System.Collections.Generic;

namespace OlaDapper.Entities
{
    public class ClassEntity
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public bool NoTimePresentation { get; set; }
        public IList<CourseEntity> Courses { get; set; }

        public ClassEntity()
        {
            Courses = new List<CourseEntity>();
        }
    }
}
