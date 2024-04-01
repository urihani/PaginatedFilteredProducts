using Ardalis.GuardClauses;
using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ProductDescription : BaseValueObject
{
    public string Value { get; private set; }
    
    private ProductDescription(){}

    public ProductDescription(string value)
    {
        Guard.Against.NullOrWhiteSpace(value, nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}