using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaginatedFilteredProducts.Domain.Products.Interfaces;
using PaginatedFilteredProducts.Infrastructure.Products.Data;
using PaginatedFilteredProducts.Infrastructure.Products.Repositories;
using Microsoft.Extensions.Configuration;

namespace PaginatedFilteredProducts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ProductsDbContext>((sp, options) =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<ProductsDbContextInitialiser>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}