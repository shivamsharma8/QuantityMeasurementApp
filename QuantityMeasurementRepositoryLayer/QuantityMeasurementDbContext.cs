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

        public DbSet<QuantityMeasurementEntity> Measurements { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuantityMeasurementEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
