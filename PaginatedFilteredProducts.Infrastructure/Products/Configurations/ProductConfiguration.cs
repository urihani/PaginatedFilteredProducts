using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Infrastructure.Products.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.Name).WithOwner();
        builder.OwnsOne(p => p.Price).WithOwner();
        builder.OwnsOne(p => p.Description).WithOwner();

        builder.HasMany(typeof(Review), "_reviews")
            .WithOne()
            .HasForeignKey("ProductId");
    }
}