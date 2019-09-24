using System.Collections.Generic;
using System.Linq;
using MeosDatabase.Entities;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.MeosAdapter.Translators
{
    static class ClassTranslator
    {
        internal static IList<CompetitionClass> ToClasses(IQueryable<ClassEntity> classEntities)
        {
            var competitionClasses = new List<CompetitionClass>();
            foreach(var classEntity in classEntities)
            {
                competitionClasses.Add(ToClass(classEntity));
            }
            return competitionClasses;
        }

        internal static CompetitionClass ToClass(ClassEntity classEntity)
        {
            return new CompetitionClass
            {
                Id = classEntity.Id,
                ShortName = classEntity.Name,
            };
        }
    }
}
