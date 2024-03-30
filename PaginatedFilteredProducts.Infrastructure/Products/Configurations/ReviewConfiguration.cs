using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaginatedFilteredProducts.Domain.Products.Aggregates;
using PaginatedFilteredProducts.Domain.Products.ValueObjects;

namespace PaginatedFilteredProducts.Infrastructure.Products.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId);

        builder.OwnsOne(r => r.Text, t =>
        {
            t.Property<string>("Value")
                .HasField("_value")
                .HasColumnName("ReviewText");
        });

        builder.OwnsOne(r => r.Rating, r =>
        {
            r.Property<int>("Value")
                .HasField("_value")
                .HasColumnName("Rating");
        });
    }
}