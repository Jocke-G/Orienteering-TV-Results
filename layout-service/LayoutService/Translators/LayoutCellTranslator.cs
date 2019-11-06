using LayoutRestService.Contracts;
using LayoutRestService.Model.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LayoutRestService.Translators
{
    public static class LayoutCellTranslator
    {
        public static IEnumerable<LayoutCell> ToContracts(this IEnumerable<LayoutCellEntity> entities)
        {
            return entities
                .OrderBy(x => x.Ordinal)
                .Select(entity => entity.ToContract())
                .ToList();
        }

        public static LayoutCell ToContract(this LayoutCellEntity entity)
        {
            return new LayoutCell
            {
                CellType = entity.CellType,
                ClassName = entity.ClassName,
                Options = entity.Options != null? JsonConvert.DeserializeObject<ClassResultOptions>(entity.Options):new ClassResultOptions(),
            };
        }

        public static IEnumerable<LayoutCellEntity> ToEntities(this IEnumerable<LayoutCell> contracts)
        {
            var entities = new List<LayoutCellEntity>();
            for (int i = 0; i < contracts.Count(); i++)
            {
                var entity = contracts.ElementAt(i).ToEntity();
                entity.Ordinal = i + 1;
                entities.Add(entity);
            }
            return entities;
        }

        public static LayoutCellEntity ToEntity(this LayoutCell contract)
        {
            return new LayoutCellEntity
            {
                CellType = contract.CellType,
                ClassName = contract.ClassName,
                Options = JsonConvert.SerializeObject(contract.Options),
            };
        }
    }
}
