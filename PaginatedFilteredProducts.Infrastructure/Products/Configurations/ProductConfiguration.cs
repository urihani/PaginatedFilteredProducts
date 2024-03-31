using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaginatedFilteredProducts.Domain.Products.Aggregates;
using PaginatedFilteredProducts.Domain.Products.ValueObjects;

namespace PaginatedFilteredProducts.Infrastructure.Products.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Id);

        builder.OwnsOne(p => p.Name, n =>
        {
            n.WithOwner();
        });

        builder.OwnsOne(p => p.Price, p =>
        {
            p.WithOwner();
        });

        builder.OwnsOne(p => p.Description, d =>
        {
            d.WithOwner();
        });
        
        var navigation = builder.Metadata.FindNavigation(nameof(Product.Reviews));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}