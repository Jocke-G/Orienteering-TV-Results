using LayoutService.Model.Contracts;
using System.Collections.Generic;

namespace LayoutService.Logic.Services
{
    public interface ILayoutService
    {
        IList<Layout> GetLayouts();
        Layout GetLayoutByName(string name);
        Layout SaveOrUpdate(Layout layout);
    }
}
