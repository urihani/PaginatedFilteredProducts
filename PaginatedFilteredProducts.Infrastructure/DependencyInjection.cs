using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaginatedFilteredProducts.Domain.Products.Interfaces;
using PaginatedFilteredProducts.Infrastructure.Products.Data;
using PaginatedFilteredProducts.Infrastructure.Products.Repositories;

namespace PaginatedFilteredProducts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductsDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("sqliteConnectionString")));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}