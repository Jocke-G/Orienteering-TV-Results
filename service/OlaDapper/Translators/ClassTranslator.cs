using OlaDapper.SimplifiedEntities;
using OrienteeringTvResults.Model;
using OrienteeringTvResults.OlaDapper.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OlaDapper.Translators
{
    public static class ClassTranslator
    {
        public static IList<CompetitionClass> ToContracts(this IList<ClassEntity> entities) {
            return entities
                .Select(x => x.ToContract())
                .ToList();
        }

        public static CompetitionClass ToContract(this ClassEntity entity)
        {
            return new CompetitionClass {
                Id = entity.Id,
                ShortName = entity.ShortName,
                NoTimePresentation = entity.NoTimePresentation,
                SplitControls = ToContract(entity.Courses),
            };
        }

        private static IList<SplitControl> ToContract(IList<CourseEntity> courses)
        {
            if(courses.Count != 1)
                return null;

            var course = courses.Single();
            return course
                .SplitControls
                .Select(x => ToContract(x))
                .ToList();
        }

        private static SplitControl ToContract(SplitControlEntity entity)
        {
            return new SplitControl
            {
                Name = entity.Name,
            };
        }
    }
}
