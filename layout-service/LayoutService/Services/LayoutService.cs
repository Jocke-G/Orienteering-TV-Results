using System.Collections.Generic;
using System.Linq;
using LayoutRestService.Contracts;

namespace LayoutRestService.Services
{
    public class LayoutService : ILayoutService
    {
        private List<Layout> _layouts;

        public LayoutService()
        {
            _layouts = new List<Layout>
            {
                new Layout("TV1")
                {
                    Rows=new List<LayoutRow>()
                    {
                        new LayoutRow(){
                            Cells = new List<LayoutCell>(){
                                new LayoutCell()
                                {
                                    ClassName = "D10",
                                },
                                new LayoutCell()
                                {
                                    ClassName = "D12",
                                },
                            },
                        },
                        new LayoutRow(){
                            Cells = new List<LayoutCell>(){
                                new LayoutCell()
                                {
                                    ClassName = "D10",
                                },
                                new LayoutCell()
                                {
                                    ClassName = "D12",
                                },
                            },
                        },
                    },
                },
                new Layout("TV2")
                {
                    Rows=new List<LayoutRow>()
                    {
                        new LayoutRow(){
                            Cells = new List<LayoutCell>(){
                                new LayoutCell()
                                {
                                    ClassName = "D14",
                                },
                                new LayoutCell()
                                {
                                    ClassName = "H14",
                                },
                            },
                        },
                    },
                },

            };
        }

        public IList<Layout> GetLayouts()
        {
            return _layouts;
        }

        public Layout GetLayoutByName(string name)
        {
            return _layouts.SingleOrDefault(x => x.Name == name);
        }
    }
}
