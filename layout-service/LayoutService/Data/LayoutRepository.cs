using System.Collections.Generic;
using System.Linq;
using LayoutRestService.Models;
using Microsoft.EntityFrameworkCore;

namespace LayoutRestService.Data
{
    public class LayoutRepository
        : ILayoutRepository
    {
        private readonly AppDbContext _context;

        public LayoutRepository(AppDbContext context)
        {
            _context = context;
        }

        public IList<LayoutEntity> GetAll()
        {
            return _context
                .Layouts
                .ToList();
        }

        public LayoutEntity GetByName(string name)
        {
            return _context
                .Layouts
                .Include(x => x.Rows)
                .ThenInclude(x => x.Cells)
                .SingleOrDefault(x => x.Name == name);
        }
    }
}
