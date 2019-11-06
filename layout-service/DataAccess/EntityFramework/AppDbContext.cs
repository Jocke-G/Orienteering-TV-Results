using LayoutService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LayoutService.DataAccess.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LayoutEntity> Layouts { get; set; }
        public DbSet<LayoutRowEntity> Rows { get; set; }
        public DbSet<LayoutCellEntity> Cells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LayoutEntity>()
                .HasMany(x => x.Rows)
                .WithOne(x => x.Layout)
                .HasForeignKey(x => x.LayoutId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LayoutRowEntity>()
                .HasIndex(x => new { x.Ordinal, x.LayoutId })
                .IsUnique();

            modelBuilder.Entity<LayoutRowEntity>()
                .HasMany(x => x.Cells)
                .WithOne(x => x.Row)
                .HasForeignKey(x => x.RowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LayoutCellEntity>()
                .HasIndex(x => new { x.Ordinal, x.RowId })
                .IsUnique();
        }
    }
}
