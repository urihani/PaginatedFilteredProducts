using MediatR;
using PaginatedFilteredProducts.Application.Products.Dtos;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Application.Products.Queries;

public class GetPaginatedProductsQuery : IRequest<PaginatedProductsResultDto>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public bool IncludeReviews { get; }

    public GetPaginatedProductsQuery(int pageNumber, int pageSize, bool includeReviews)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        IncludeReviews = includeReviews;
    }
}