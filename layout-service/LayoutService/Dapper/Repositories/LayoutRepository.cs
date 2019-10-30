using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LayoutRestService.Models;
using LayoutRestService.RepositoryInterfaces;

namespace LayoutRestService.Dapper.Repositories
{
    public class LayoutRepository
        : ILayoutRepository
    {
        private IDbConnection _connection;

        public LayoutRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IList<LayoutEntity> GetAll()
        {
            string sql = "SELECT * FROM layout";
            var layouts = _connection.Query<LayoutEntity>(sql);
            return layouts.ToList();
        }

        public LayoutEntity GetByName(string name)
        {
            var sql = @"SELECT layout.id,
layout.name,
layout_row.id,
layout_cell.id,
layout_cell.cell_type AS CellType,
layout_cell.class_name AS ClassName
FROM layout
LEFT JOIN layout_row
    ON layout_row.layout_id = layout.id
LEFT JOIN layout_cell
    ON layout_cell.row_id = layout_row.id
WHERE name = @name";
            var param = new { name };
            var splitOn = "id,id";

            LayoutEntity layoutEntry = null;

            var layouts = _connection.Query<LayoutEntity, LayoutRowEntity, LayoutCellEntity, LayoutEntity>(sql, (layout, layoutRow, layoutCell) =>
            {
                if(layoutEntry == null)
                {
                    layoutEntry = layout;
                }

                var existingRow = layoutEntry.Rows.SingleOrDefault(x => x.Id == layoutRow.Id);

                if (existingRow == null)
                {
                    existingRow = layoutRow;
                    layoutEntry.Rows.Add(layoutRow);
                }

                if(layoutCell != null)
                    layoutRow.Cells.Add(layoutCell);

                return layoutEntry;
            },
            param: param,
            splitOn: splitOn
            );

            return layouts.FirstOrDefault();
        }
    }
}
