using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Infrastructure.Products.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        // Assuming Review has a reference back to Product, configure the foreign key
        builder.HasOne<Product>().WithMany("_reviews").HasForeignKey("ProductId");

        // Configure fields for the Review entity, such as text, rating, etc.
        builder.OwnsOne(r => r.Text).WithOwner();
        builder.OwnsOne(r => r.Rating).WithOwner();
    }
}