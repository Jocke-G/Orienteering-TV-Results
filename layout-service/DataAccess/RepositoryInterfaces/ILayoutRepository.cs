using LayoutService.DataAccess.Entities;
using System.Collections.Generic;

namespace LayoutService.DataAccess.RepositoryInterfaces
{
    public interface ILayoutRepository
    {
        LayoutEntity Create(LayoutEntity layout);
        IEnumerable<LayoutEntity> GetAll();
        LayoutEntity GetByName(string name);
        LayoutEntity Update(LayoutEntity layoutEntity);
    }
}
