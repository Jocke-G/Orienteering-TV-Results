using LayoutRestService.Data;
using LayoutRestService.Models;
using System.Collections.Generic;

namespace LayoutRestService.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            db.Layouts.AddRange(GetLAyouts());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext db)
        {
            db.Layouts.RemoveRange(db.Layouts);
            InitializeDbForTests(db);
        }

        public static List<LayoutEntity> GetLAyouts()
        {
            return new List<LayoutEntity>()
            {
                new LayoutEntity(){ Name = "TestTV1" },
                new LayoutEntity(){ Name = "TestTV2" },
                new LayoutEntity(){ Name = "TestTV3" }
            };
        }
    }
}
