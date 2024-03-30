using PaginatedFilteredProducts.Domain.Products.Interfaces;
using PaginatedFilteredProducts.Infrastructure.Products.Data;

namespace PaginatedFilteredProducts.Infrastructure.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductsDbContext _context;

    public ProductRepository(ProductsDbContext context)
    {
        _context = context;
    }
}