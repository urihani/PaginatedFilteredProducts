using Ardalis.Specification;

namespace PaginatedFilteredProducts.Domain.Common.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}