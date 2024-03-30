using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ProductName : BaseValueObject
{
    private string _value;
    
    public string Value
    {
        get => _value;
        private set => _value = value; // Keep setter private to enforce encapsulation
    }
    
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