using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Infrastructure.Products.Data;

public class ProductsDbContextInitialiser
{
    private readonly ILogger<ProductsDbContextInitialiser> _logger;
    private readonly ProductsDbContext _context;

    public ProductsDbContextInitialiser(ILogger<ProductsDbContextInitialiser> logger,
        ProductsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Products.Any())
        {
            var products = new List<Product>();

            for (int i = 1; i <= 300; i++)
            {
                var product = ProductFactory.CreateProduct(
                    name: $"Product {i}",
                    amount: 100m + i, // Example pricing logic
                    currency: "USD",
                    description: $"Description for Product {i}");

                // Add some reviews for each product
                for (int j = 1; j <= 5; j++) // Example: 5 reviews per product
                {
                    var review = ReviewFactory.CreateReview(
                        text: $"Review {j} for Product {i}",
                        rating: j % 5 + 1); // Cycle through ratings 1-5

                    product.AddReview(review);
                }

                products.Add(product);
            }

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }
}