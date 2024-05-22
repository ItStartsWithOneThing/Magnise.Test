
using Magnise.Test.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magnise.Test.DAL
{
    public class CryptoDBContext : DbContext
    {
        public CryptoDBContext(DbContextOptions<CryptoDBContext> options) : base(options)
        {
        }

        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cryptocurrency>(entity =>
            {
                entity.HasKey(e => e.ID).IsClustered(true);

                entity.Property(e => e.ID).ValueGeneratedOnAdd(); // Adding IDENTITY constraint

                entity.HasIndex(e => e.AssetID, "IX_Cryptocurrencies_AssetID")
                    .IsUnique();
            });
        }
    }
}
