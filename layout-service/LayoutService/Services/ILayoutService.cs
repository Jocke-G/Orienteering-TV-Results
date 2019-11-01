using System.Collections.Generic;
using LayoutRestService.Contracts;

namespace LayoutRestService.Services
{
    public interface ILayoutService
    {
        IList<Layout> GetLayouts();
        Layout GetLayoutByName(string name);
        Layout SaveOrUpdate(Layout layout);
    }
}
