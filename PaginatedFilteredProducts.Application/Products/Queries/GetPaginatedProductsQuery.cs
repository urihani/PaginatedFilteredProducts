using MediatR;
using PaginatedFilteredProducts.Application.Products.Dtos;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Application.Products.Queries;

public class GetPaginatedProductsQuery : IRequest<PaginatedProductsResultDto>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public bool IncludeReviews { get; }
    public (string Column, string SortDirection) SortInstruction { get; }
    public Dictionary<string, List<(string Operation, object Value)>> FilterCriteria { get; }

    public GetPaginatedProductsQuery(int pageNumber, int pageSize, bool includeReviews, (string Column, string SortDirection) sortInstruction, Dictionary<string, List<(string Operation, object Value)>> filterCriteria)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        IncludeReviews = includeReviews;
        SortInstruction = sortInstruction;
        FilterCriteria = filterCriteria;
    }
}