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
            n.Property<string>("Value")
                .HasField("_value")
                .HasColumnName("Name");
        });

        builder.OwnsOne(p => p.Price, p =>
        {
            p.Property<decimal>("Amount")
                .HasField("_amount")
                .HasColumnName("PriceAmount");

            p.Property<string>("Currency")
                .HasField("_currency")
                .HasColumnName("PriceCurrency");
        });

        builder.OwnsOne(p => p.Description, d =>
        {
            d.Property<string>("Value")
                .HasField("_value")
                .HasColumnName("Description");
        });

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);
    }
}