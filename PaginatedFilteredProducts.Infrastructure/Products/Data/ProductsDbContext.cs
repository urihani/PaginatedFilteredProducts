using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PaginatedFilteredProducts.Domain.Products.Aggregates;
using PaginatedFilteredProducts.Infrastructure.Products.Configurations;

namespace PaginatedFilteredProducts.Infrastructure.Products.Data;

public class ProductsDbContext : DbContext
{
        public DbSet<Product>? Products { get; set; }
        public DbSet<Review>? Reviews { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }
}