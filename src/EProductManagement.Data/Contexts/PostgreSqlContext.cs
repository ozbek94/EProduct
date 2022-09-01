using EProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EProductManagement.Data.Contexts
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
            : base(options)
        {

        }
        public DbSet<EProduct> EProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Redemption> Redemptions { get; set; }
        public DbSet<ProductBalance> ProductBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
