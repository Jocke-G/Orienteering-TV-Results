using System.Collections.Generic;

namespace LayoutService.Model.Contracts
{
    public class Layout
    {
        public string Name { get; set; }
        public IList<LayoutRow> Rows { get; set; }

        public Layout()
        {
            Rows = new List<LayoutRow>();
        }

        public Layout(string name)
            :this()
        {
            Name = name;
        }
    }
}
