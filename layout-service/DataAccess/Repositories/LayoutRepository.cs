﻿using LayoutService.DataAccess.Entities;
using LayoutService.DataAccess.EntityFramework;
using LayoutService.DataAccess.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LayoutService.DataAccess.Repositories
{
    public class LayoutRepository
        : ILayoutRepository
    {
        private readonly AppDbContext _context;

        public LayoutRepository(AppDbContext context)
        {
            _context = context;
        }

        public LayoutEntity Create(LayoutEntity layout)
        {
            var result = _context.Add(layout);
            _context.SaveChanges();
            return result.Entity;
        }

        public IEnumerable<LayoutEntity> GetAll()
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

        public LayoutEntity Update(LayoutEntity layoutEntity)
        {
            var result = _context.Update(layoutEntity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
