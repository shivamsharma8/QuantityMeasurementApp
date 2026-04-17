using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementRepositoryLayer
{
    public class HistoryDbContext : DbContext
    {
        public HistoryDbContext(DbContextOptions<HistoryDbContext> options) : base(options)
        {
        }

        public DbSet<QuantityMeasurementEntity> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuantityMeasurementEntity>().HasKey(e => e.Id);
        }
    }
}
