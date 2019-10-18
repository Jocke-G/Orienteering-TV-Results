using System.Collections.Generic;
using LayoutRestService.Contracts;

namespace LayoutRestService.Services
{
    internal interface ILayoutService
    {
        IList<Layout> GetLayouts();
        Layout GetLayoutByName(string name);
    }
}
