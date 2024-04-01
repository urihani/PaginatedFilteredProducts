using PaginatedFilteredProducts.Services.QueryParameterParser;

namespace PaginatedFilteredProducts;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddSingleton<IQueryParameterParser, QueryParameterParser>();

        return services;
    }
}