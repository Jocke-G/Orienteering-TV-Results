using System.Collections.Generic;

namespace LayoutService.Model.Contracts
{
    public class LayoutRow
    {
        public IList<LayoutCell> Cells { get; set; }

        public LayoutRow()
        {
            Cells = new List<LayoutCell>();
        }
    }
}
