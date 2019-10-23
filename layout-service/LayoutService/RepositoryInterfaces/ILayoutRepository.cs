using LayoutRestService.Models;
using System.Collections.Generic;

namespace LayoutRestService.RepositoryInterfaces
{
    public interface ILayoutRepository
    {
        IList<LayoutEntity> GetAll();
        LayoutEntity GetByName(string name);
    }
}
