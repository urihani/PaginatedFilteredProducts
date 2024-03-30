using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ReviewRating : BaseValueObject
{
    private int _value;
    
    public int Value
    {
        get => _value;
        private set => _value = value; // Keep setter private to enforce encapsulation
    }
    
    private ReviewRating(){}

    public ReviewRating(int value)
    {
        if (value < 1 || value > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 1 and 5");
        }

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}