using System.Collections.Generic;
using System.Linq;
using LayoutRestService.Contracts;
using LayoutRestService.Models;
using LayoutRestService.RepositoryInterfaces;

namespace LayoutRestService.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ILayoutRepository _layoutRepository;

        public LayoutService(ILayoutRepository layoutRepository)
        {
            _layoutRepository = layoutRepository;
        }

        public IList<Layout> GetLayouts()
        {
            var entities = _layoutRepository.GetAll();
            return ToContracts(entities);
        }

        public Layout GetLayoutByName(string name)
        {
            var entity = _layoutRepository.GetByName(name);
            if (entity == null)
                return null;

            return ToContract(entity);
        }

        private IList<Layout> ToContracts(IList<LayoutEntity> entities)
        {
            return entities.Select(entity => ToContract(entity)).ToList();
        }

        private Layout ToContract(LayoutEntity entity)
        {
            return new Layout
            {
                Name = entity.Name,
                Rows = ToContracts(entity.Rows),
            };
        }

        private IList<LayoutRow> ToContracts(ICollection<LayoutRowEntity> entities)
        {
            return entities
                .OrderBy(x => x.Ordinal)
                .Select(entity => ToContract(entity))
                .ToList();
        }

        private LayoutRow ToContract(LayoutRowEntity entity)
        {
            return new LayoutRow {
                Cells = ToContracts(entity.Cells)
            };
        }

        private IList<LayoutCell> ToContracts(ICollection<LayoutCellEntity> entities)
        {
            return entities
                .OrderBy(x => x.Ordinal)
                .Select(entity => ToContract(entity))
                .ToList();
        }

        private LayoutCell ToContract(LayoutCellEntity entity)
        {
            return new LayoutCell
            {
                ClassName = entity.ClassName,
            };
        }
    }
}
