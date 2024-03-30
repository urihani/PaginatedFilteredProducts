using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ProductDescription : BaseValueObject
{
    private string Value { get; set; }
    
    private ProductDescription(){}

    public ProductDescription(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}