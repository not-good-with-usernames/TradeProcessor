using Microsoft.EntityFrameworkCore;
using TradeProcessor.Models;

namespace TradeProcessor.Repository
{
    public class TradeContext : DbContext
    {
        public TradeContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<TradeRecord>()
            //    .Property(p => p.Price)
            //    .HasColumnType("decimal(10, 10)");

            //modelBuilder.Entity<TradeRecord>()
            //    .Property(p => p.Lots)
            //    .HasColumnType("NUMERIC(10, 10)");
        }

        private DbSet<TradeRecord> Trades { get; set; }
    }
}
