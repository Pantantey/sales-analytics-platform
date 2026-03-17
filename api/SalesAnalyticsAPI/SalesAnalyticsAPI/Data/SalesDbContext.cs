using Microsoft.EntityFrameworkCore;
using SalesAnalyticsAPI.Models;

namespace SalesAnalyticsAPI.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<FactSales> FactSales { get; set; }
        public DbSet<DimCustomer> Customers { get; set; }
        public DbSet<DimProduct> Products { get; set; }
        public DbSet<DimDate> Dates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapear las entidades a las tablas reales del Data Warehouse
            modelBuilder.Entity<FactSales>().ToTable("FactSales");
            modelBuilder.Entity<DimCustomer>().ToTable("DimCustomer");
            modelBuilder.Entity<DimProduct>().ToTable("DimProduct");
            modelBuilder.Entity<DimDate>().ToTable("DimDate");
        }
    }
}