using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutRestService.Model.Entities
{
    [Table("layout")]
    public class LayoutEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<LayoutRowEntity> Rows { get; set; }

        public LayoutEntity()
        {
            Rows = new List<LayoutRowEntity>();
        }
    }
}
