using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ProductName : BaseValueObject
{
    private string Value { get; set; }
    
    private ProductName(){}

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Product name cannot be empty");
        }

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}