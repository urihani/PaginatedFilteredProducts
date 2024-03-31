using PaginatedFilteredProducts.Domain.Common;
using PaginatedFilteredProducts.Domain.Common.Abstractions;
using PaginatedFilteredProducts.Domain.Common.Interfaces;
using PaginatedFilteredProducts.Domain.Products.ValueObjects;

namespace PaginatedFilteredProducts.Domain.Products.Aggregates;

public static class ProductFactory
{
    public static Product CreateProduct(string name, decimal amount, string currency, string description)
    {
        var productId = Guid.NewGuid();
        var productName = new ProductName(name);
        var price = new Money(amount, currency);
        var productDescription = new ProductDescription(description);

        return new Product(productId, productName, price, productDescription);
    }
}

public class Product: BaseEntity, IAggregateRoot
{
    public new Guid Id { get; private set; }
    public ProductName Name { get; private set; }
    public Money Price { get; private set; }
    public ProductDescription Description { get; private set; }
    private readonly List<Review> _reviews = new List<Review>();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    
    private Product() { }

    public Product(Guid id, ProductName name, Money price, ProductDescription description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public void AddReview(Review review)
    {
        _reviews.Add(review);
    }
}