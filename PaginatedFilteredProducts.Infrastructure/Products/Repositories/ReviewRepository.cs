using PaginatedFilteredProducts.Domain.Products.Interfaces;
using PaginatedFilteredProducts.Infrastructure.Products.Data;

namespace PaginatedFilteredProducts.Infrastructure.Products.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ProductsDbContext _context;

    public ReviewRepository(ProductsDbContext context)
    {
        _context = context;
    }
}