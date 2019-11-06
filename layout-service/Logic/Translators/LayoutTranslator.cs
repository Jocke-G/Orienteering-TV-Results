using LayoutService.DataAccess.Entities;
using LayoutService.Model.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LayoutService.Logic.Translators
{
    public static class LayoutTranslator
    {
        public static IEnumerable<Layout> ToContracts(this IEnumerable<LayoutEntity> entities)
        {
            return entities.Select(entity => entity.ToContract()).ToList();
        }

        public static Layout ToContract(this LayoutEntity entity)
        {
            return new Layout
            {
                Name = entity.Name,
                Rows = entity.Rows.ToContracts().ToList(),
            };
        }

        public static LayoutEntity ToEntity(this Layout layout)
        {
            return new LayoutEntity
            {
                Name = layout.Name,
                Rows = layout.Rows.ToEntities()
            };
        }
    }
}
