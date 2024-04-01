using Microsoft.Extensions.DependencyInjection;

namespace PaginatedFilteredProducts.Infrastructure.Products.Data;

public class DatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InitialiseDatabaseAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ProductsDbContextInitialiser>();

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}