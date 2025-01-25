using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        // DbSet representing the Products table
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring ProductId to be a 6-digit unique number
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd();

            // Optional: Seeding initial data if required
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 100001,
                    Name = "Sample Product 1",
                    Description = "This is a sample product.",
                    Price = 49.99m,
                    StockAvailable = 100
                },
                new Product
                {
                    ProductId = 100002,
                    Name = "Sample Product 2",
                    Description = "This is another sample product.",
                    Price = 99.99m,
                    StockAvailable = 50
                }
            );
        }
    }
}