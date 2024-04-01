using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using PaginatedFilteredProducts.Application.Products.Dtos;
using PaginatedFilteredProducts.Domain.Common.Interfaces;
using PaginatedFilteredProducts.Domain.Products.Aggregates;
using PaginatedFilteredProducts.Domain.Products.Specifications;

namespace PaginatedFilteredProducts.Application.Products.Queries;

public class GetPaginatedProductsQueryHandler : IRequestHandler<GetPaginatedProductsQuery, PaginatedProductsResultDto>
{
    private readonly IReadRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetPaginatedProductsQueryHandler(IReadRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedProductsResultDto> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        
        if (!string.IsNullOrWhiteSpace(request.SortInstruction.SortDirection))
        {
            Guard.Against.NullOrEmpty(request.SortInstruction.Column, nameof(request.SortInstruction.Column));
        }
        
        var productsSpec = new ProductsPaginatedSpecification(
            skip: request.PageNumber * request.PageSize,
            take: request.PageSize,
            includeReviews: request.IncludeReviews,
            sortInstruction: (request.SortInstruction.Column, request.SortInstruction.SortDirection),
            filterCriteria: request.FilterCriteria);
        
        var products = await _productRepository.ListAsync(productsSpec, cancellationToken);

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        var totalCount = await _productRepository.CountAsync(cancellationToken);

        return new PaginatedProductsResultDto
        {
            Products = productDtos,
            TotalCount = totalCount
        };
    }
}