using OlaDapper.Entities;
using OrienteeringTvResults.Model;
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
            };
        }
    }
}
