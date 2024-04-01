using System.Linq.Expressions;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Domain.Products.Specifications;

public sealed class ProductsPaginatedSpecification : Specification<Product>
{
    private readonly Dictionary<string, Expression<Func<Product, object>>> _sortExpressions;
    public ProductsPaginatedSpecification(int skip,
        int take,
        bool includeReviews = false,
        (string Column, string SortDirection)? sortInstruction = null,
        Dictionary<string, List<(string Operation, object Value)>> filterCriteria = null)
    {
        _sortExpressions = new Dictionary<string, Expression<Func<Product, object>>>
        {
            {"name", p => p.Name.Value},
            {"price", p => p.Price.Amount},
            {"currency", p => p.Price.Currency},
            {"description", p => p.Description.Value}
        };
        
        ApplyPagination(skip, take);
        ApplyIncludes(includeReviews);
        ApplySorting(sortInstruction.Value);
        ApplyFiltering(filterCriteria);
    }
    
    private void ApplyPagination(int skip, int take)
    {
        if (take <= 0) take = int.MaxValue;

        Query.Skip(skip).Take(take);
    }

    private void ApplyIncludes(bool includeReviews)
    {
        if (includeReviews)
        {
            Query.Include(p => p.Reviews);
        }
    }

    private void ApplySorting((string Column, string SortDirection) sortInstruction)
    {
        var (column, sortDirection) = sortInstruction;

        var propertyName = column.Contains('.') ? column.Substring(0, column.IndexOf('.')) : column;

        if (!_sortExpressions.TryGetValue(propertyName.ToLower(), out var sortingExpression)) return;
        Guard.Against.Null(sortingExpression, nameof(sortingExpression));
            
        if (sortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
        {
            Query.OrderBy(sortingExpression!);
        }
        else if (sortDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase))
        {
            Query.OrderByDescending(sortingExpression!);
        }
    }
    
    private void ApplyFiltering(Dictionary<string, List<(string Operation, object Value)>> filterCriteria)
    {
        foreach (var (column, criteria) in filterCriteria)
        {
            foreach (var (operation, value) in criteria)
            {
                Guard.Against.Null(value, nameof(value));
                Guard.Against.Null(operation, nameof(operation));
                
                switch (column.ToLower())
                {
                    case "name":
                        ApplyStringFilter(nameof(Product.Name), operation, value.ToString());
                        break;
                    case "description":
                        ApplyStringFilter(nameof(Product.Description), operation, value.ToString());
                        break;
                    case "price":
                        ApplyNumericFilter(nameof(Product.Price.Amount), operation, Convert.ToDecimal(value));
                        break;
                    case "currency":
                        if (operation == "equals")
                        {
                            Query.Where(p => p.Price.Currency == value.ToString());
                        }
                        break;
                }
            }
        }
    }

    private void ApplyStringFilter(string propertyName, string operation, string? value)
    {
        var parameter = Expression.Parameter(typeof(Product), "p");
        var property = Expression.Property(parameter, propertyName);
        var valueConstant = Expression.Constant(value, typeof(string));
        Expression operationExpression = operation.ToLower() switch
        {
            "startswith" => Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), valueConstant),
            "contains" => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueConstant),
            "notcontains" => Expression.Not(Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueConstant)),
            "endswith" => Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), valueConstant),
            "equals" => Expression.Equal(property, valueConstant),
            _ => throw new ArgumentException($"Unsupported string operation '{operation}'.")
        };

        if (operationExpression != null)
        {
            var lambda = Expression.Lambda<Func<Product, bool>>(operationExpression, parameter);
            Query.Where(lambda);
        }
    }

    private void ApplyNumericFilter<TValue>(
        string propertyName, 
        string operation, 
        TValue value) where TValue : struct, IComparable, IComparable<TValue>
    {
        var parameter = Expression.Parameter(typeof(Product), "p");
        var property = Expression.Property(parameter, propertyName);
        var valueConstant = Expression.Constant(value, typeof(TValue));

        // Ensure the property and value are comparable
        var conversion = Expression.Convert(property, typeof(TValue));
    
        Expression operationExpression = operation switch
        {
            "equals" => Expression.Equal(conversion, valueConstant),
            "notequals" => Expression.NotEqual(conversion, valueConstant),
            "lessthan" => Expression.LessThan(conversion, valueConstant),
            "lessthanorequalto" => Expression.LessThanOrEqual(conversion, valueConstant),
            "greaterthan" => Expression.GreaterThan(conversion, valueConstant),
            "greaterthanorequalto" => Expression.GreaterThanOrEqual(conversion, valueConstant),
            _ => throw new ArgumentException($"Unsupported operation '{operation}'.")
        };

        var lambda = Expression.Lambda<Func<Product, bool>>(operationExpression, parameter);
        Query.Where(lambda);
    }
}