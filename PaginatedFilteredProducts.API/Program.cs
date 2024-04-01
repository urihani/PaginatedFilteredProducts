using MediatR;
using PaginatedFilteredProducts.Application;
using PaginatedFilteredProducts.Application.Products.Queries;
using PaginatedFilteredProducts.Infrastructure;
using PaginatedFilteredProducts.Infrastructure.Products.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

async Task InitialiseDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ProductsDbContextInitialiser>();

    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    await InitialiseDatabaseAsync(app);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (IMediator mediator, int pageNumber, int pageSize, bool includeReviews = false) =>
    {
        var query = new GetPaginatedProductsQuery(pageNumber, pageSize, includeReviews);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    })
    .WithName("GetPaginatedProducts");

app.Run();