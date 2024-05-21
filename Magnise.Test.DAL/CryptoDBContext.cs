
using Magnise.Test.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magnise.Test.DAL
{
    public class CryptoDBContext : DbContext
    {
        public CryptoDBContext(DbContextOptions<CryptoDBContext> options) : base(options)
        {
        }

        DbSet<Cryptocurrency> Cryptocurrencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cryptocurrency>(entity =>
            {
                entity.HasKey(e => e.ID).IsClustered(true);

                entity.HasIndex(e => e.Name, "IX_Cryptocurrencies_Name")
                    .IsUnique();
            });
        }
    }
}
