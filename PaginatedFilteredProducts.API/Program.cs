using MediatR;
using Microsoft.OpenApi.Models;
using PaginatedFilteredProducts;
using PaginatedFilteredProducts.Application;
using PaginatedFilteredProducts.Application.Products.Dtos;
using PaginatedFilteredProducts.Application.Products.Queries;
using PaginatedFilteredProducts.Infrastructure;
using PaginatedFilteredProducts.Infrastructure.Products.Data;
using PaginatedFilteredProducts.Services.QueryParameterParser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
    await databaseInitializer.InitialiseDatabaseAsync();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (IMediator mediator, IQueryParameterParser queryParser, int pageNumber, int pageSize, bool includeReviews = false, string sort = "name,asc", string filter = "") =>
    {
        var sortInstruction = queryParser.ParseSortInstruction(sort);
        var filterCriteria = queryParser.ParseFilterCriteria(filter);

        var query = new GetPaginatedProductsQuery(
            pageNumber,
            pageSize,
            includeReviews,
            sortInstruction ?? (Column: string.Empty, SortDirection: "asc"),
            filterCriteria);

        var result = await mediator.Send(query);
        return Results.Ok(result);
    })
    .WithName("GetPaginatedProducts")
    .WithMetadata(new EndpointNameMetadata("GetPaginated Products"))
    .Produces<PaginatedProductsResultDto>(StatusCodes.Status200OK, "application/json")
    .WithMetadata(new OpenApiOperation
    {
        Summary = "Retrieve paginated products",
        Description = "Retrieves paginated products with optional sorting and filtering.",
        OperationId = "GetPaginatedProducts",
        Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Products" } },
        Parameters = new List<OpenApiParameter>
        {
            new OpenApiParameter { Name = "pageNumber", In = ParameterLocation.Query, Required = true, Description = "The page number for pagination. <br /> - Begins at 0 <br />", Schema = new OpenApiSchema { Type = "integer" } },
            new OpenApiParameter { Name = "pageSize", In = ParameterLocation.Query, Required = true, Description = "The size of each page for pagination. <br /> - 0 to display all results", Schema = new OpenApiSchema { Type = "integer" } },
            new OpenApiParameter { Name = "includeReviews", In = ParameterLocation.Query, Description = "Whether to include reviews for each product.", Schema = new OpenApiSchema { Type = "boolean" } },
            new OpenApiParameter { Name = "sort", In = ParameterLocation.Query, Description = "Sorting instructions, formatted as 'columnName,asc|desc'. <br /> - Name: 'name,asc - name,desc' <br /> - Price: 'price,asc - price,desc' <br /> - Currency: 'currency,asc - currency,desc' <br /> - Description: 'description,asc - description,desc'", Schema = new OpenApiSchema { Type = "string" } },
            new OpenApiParameter { Name = "filter", In = ParameterLocation.Query, Description = "Filtering criteria, formatted as 'columnName,operation,value'. Multiple criteria can be separated by ';'. Supported operations for string columns: 'equals', 'contains', 'startswith', 'endswith'. For numeric columns: 'equals', 'lessthan', 'greaterthan'. Example: 'name,contains,Glove;price,greaterthan,100'", Schema = new OpenApiSchema { Type = "string" } }
        }
    });

app.Run();