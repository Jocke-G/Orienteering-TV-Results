using LayoutRestService.Model.Entities;
using System.Collections.Generic;

namespace LayoutRestService.RepositoryInterfaces
{
    public interface ILayoutRepository
    {
        LayoutEntity Create(LayoutEntity layout);
        IEnumerable<LayoutEntity> GetAll();
        LayoutEntity GetByName(string name);
        LayoutEntity Update(LayoutEntity layoutEntity);
    }
}
