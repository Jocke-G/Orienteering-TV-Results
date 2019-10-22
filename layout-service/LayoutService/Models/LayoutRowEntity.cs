using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutRestService.Models
{
    [Table("layout_row")]
    public class LayoutRowEntity
    {
        public int Id { get; set; }
        [Column("layout_id")]
        public int LayoutId { get; internal set; }
        public LayoutEntity Layout { get; set; }
        public ICollection<LayoutCellEntity> Cells { get; set; }
        public int Ordinal { get; set; }

        public LayoutRowEntity()
        {
            Cells = new List<LayoutCellEntity>();
        }
    }
}
