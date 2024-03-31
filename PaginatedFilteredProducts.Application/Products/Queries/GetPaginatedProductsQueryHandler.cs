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
        // Create a specification for paginated products
        var productsSpec = new ProductsPaginatedSpecification(request.PageNumber * request.PageSize, request.PageSize,  request.IncludeReviews);
        
        // Fetch products using the specification
        var products = await _productRepository.ListAsync(productsSpec, cancellationToken);

        // Map domain entities to DTOs
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        // Optionally, get the total count for pagination purposes
        var totalCount = await _productRepository.CountAsync(cancellationToken);

        // Return the paginated result as DTO
        return new PaginatedProductsResultDto
        {
            Products = productDtos,
            TotalCount = totalCount
        };
    }
}