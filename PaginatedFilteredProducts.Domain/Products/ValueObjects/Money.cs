using PaginatedFilteredProducts.Domain.Common.Abstractions;

namespace PaginatedFilteredProducts.Domain.Products.ValueObjects;

public class Money : BaseValueObject
{
    private decimal _amount;
    private string _currency;
    
    public decimal Amount
    {
        get => _amount;
        private set => _amount = value; // Keep setter private to enforce encapsulation
    }
    
    public string Currency
    {
        get => _currency;
        private set => _currency = value; // Keep setter private to enforce encapsulation
    }
    
    private Money(){}

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }

        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}