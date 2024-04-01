using Ardalis.GuardClauses;
using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class ReviewRating : BaseValueObject
{
    public int Value { get; private set; }
    
    private ReviewRating(){}

    public ReviewRating(int value)
    {
        Guard.Against.OutOfRange(value, nameof(value), 1, 5);

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}