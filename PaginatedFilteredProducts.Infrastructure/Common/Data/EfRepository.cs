using Ardalis.Specification.EntityFrameworkCore;
using PaginatedFilteredProducts.Domain.Common.Interfaces;
using PaginatedFilteredProducts.Infrastructure.Products.Data;

namespace PaginatedFilteredProducts.Infrastructure.Common.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ProductsDbContext dbContext) : base(dbContext)
    {
    }
}