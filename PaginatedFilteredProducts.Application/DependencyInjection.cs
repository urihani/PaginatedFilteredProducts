using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PaginatedFilteredProducts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper Configuration
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // MediatR Configuration for CQRS
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}