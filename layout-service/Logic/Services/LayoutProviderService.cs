using System.Collections.Generic;
using System.Linq;
using LayoutService.DataAccess.RepositoryInterfaces;
using LayoutService.Logic.Translators;
using LayoutService.Model.Contracts;

namespace LayoutService.Logic.Services
{
    public class LayoutProviderService
        : ILayoutService
    {
        private readonly ILayoutRepository _layoutRepository;

        public LayoutProviderService(ILayoutRepository layoutRepository)
        {
            _layoutRepository = layoutRepository;
        }

        public IList<Layout> GetLayouts()
        {
            return _layoutRepository
                .GetAll()
                .ToContracts()
                .ToList();
        }

        public Layout GetLayoutByName(string name)
        {
            var entity = _layoutRepository.GetByName(name);
            if (entity == null)
                return null;

            return entity.ToContract();
        }

        public Layout SaveOrUpdate(Layout layout)
        {
            var existingLayout = _layoutRepository.GetByName(layout.Name);

            if(existingLayout == null)
            {
                return _layoutRepository
                    .Create(layout.ToEntity())
                    .ToContract();
            }

            existingLayout.Rows = layout.Rows.ToEntities();
            return _layoutRepository
                .Update(existingLayout)
                .ToContract();
        }
    }
}
