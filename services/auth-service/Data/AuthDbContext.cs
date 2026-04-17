using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer
{
    public class QuantityMeasurementDbContext : DbContext
    {
        public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
