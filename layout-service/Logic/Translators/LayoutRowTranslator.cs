using LayoutService.DataAccess.Entities;
using LayoutService.Model.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LayoutService.Logic.Translators
{
    public static class LayoutRowTranslator
    {
        public static IEnumerable<LayoutRow> ToContracts(this IEnumerable<LayoutRowEntity> entities)
        {
            return entities
                .OrderBy(x => x.Ordinal)
                .Select(entity => ToContract(entity))
                .ToList();
        }

        public static LayoutRow ToContract(this LayoutRowEntity entity)
        {
            return new LayoutRow
            {
                Cells = entity.Cells.ToContracts().ToList(),
            };
        }

        public static IEnumerable<LayoutRowEntity> ToEntities(this IEnumerable<LayoutRow> contracts)
        {
            var entities = new List<LayoutRowEntity>();
            for (int i = 0; i < contracts.Count(); i++)
            {
                var entity = contracts.ElementAt(i).ToEntity();
                entity.Ordinal = i + 1;
                entities.Add(entity);
            }
            return entities;
        }

        public static LayoutRowEntity ToEntity(this LayoutRow contract)
        {
            return new LayoutRowEntity
            {
                Cells = contract.Cells.ToEntities(),
            };
        }
    }
}
