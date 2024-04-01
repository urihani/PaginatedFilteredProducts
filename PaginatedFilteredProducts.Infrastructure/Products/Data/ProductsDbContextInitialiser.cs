using Bogus;
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
        if (!_context.Products.Any())
        {
            var products = new List<Product>();

            var currencies = new[] { "USD", "EUR" };

            var faker = new Faker();

            for (int i = 1; i <= 300; i++)
            {
                var name = faker.Commerce.ProductName();
                var amount = faker.Random.Double(50, 150);
                var currency = faker.PickRandom(currencies);
                var description = faker.Commerce.ProductDescription();

                var product = ProductFactory.CreateProduct(name, amount, currency, description);

                for (int j = 1; j <= 5; j++)
                {
                    var reviewText = faker.Lorem.Sentence();
                    var reviewRating = faker.Random.Int(1, 5);
                    var review = ReviewFactory.CreateReview(reviewText, reviewRating);

                    product.AddReview(review);
                }

                products.Add(product);
            }

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }
}