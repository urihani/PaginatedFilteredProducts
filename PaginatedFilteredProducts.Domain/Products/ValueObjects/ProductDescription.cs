using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ProductDescription : BaseValueObject
{
    private string _value;
    
    public string Value
    {
        get => _value;
        private set => _value = value; // Keep setter private to enforce encapsulation
    }
    
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