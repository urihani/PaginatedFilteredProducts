using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaginatedFilteredProducts.Infrastructure.Products.Data;
using Microsoft.Extensions.Configuration;
using PaginatedFilteredProducts.Domain.Common.Interfaces;
using PaginatedFilteredProducts.Infrastructure.Common.Data;

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
        
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        return services;
    }
}