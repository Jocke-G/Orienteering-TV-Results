using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutRestService.Models
{
    [Table("layout")]
    public class LayoutEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LayoutRowEntity> Rows { get; set; }

        public LayoutEntity()
        {
            Rows = new List<LayoutRowEntity>();
        }
    }
}
