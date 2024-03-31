using Ardalis.Specification;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Domain.Products.Specifications;

public sealed class ProductsPaginatedSpecification : Specification<Product>
{
    public ProductsPaginatedSpecification(int skip, int take, bool includeReviews = false)
    {
        if (take <= 0) take = int.MaxValue;

        Query.Skip(skip).Take(take);

        if (includeReviews)
        {
            Query.Include(p => p.Reviews);
        }
    }
}