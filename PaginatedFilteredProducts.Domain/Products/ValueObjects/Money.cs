using Ardalis.GuardClauses;
using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class Money : BaseValueObject
{
    public double Amount { get; private set; }
    public string Currency { get; private set; }
    
    private Money(){}

    public Money(double amount, string currency)
    {
        Guard.Against.NegativeOrZero(amount, nameof(amount));

        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}