﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutService.DataAccess.Entities
{
    [Table("layout_cell")]

    public class LayoutCellEntity
    {
        public int Id { get; set; }
        [Column("row_id")]
        public int RowId { get; internal set; }
        public LayoutRowEntity Row { get; set; }
        public int Ordinal { get; set; }
        [Column("cell_type")]
        public string CellType { get; set; }
        [Column("class_name")]
        public string ClassName { get; set; }
        public string Options { get; set; }
    }
}
