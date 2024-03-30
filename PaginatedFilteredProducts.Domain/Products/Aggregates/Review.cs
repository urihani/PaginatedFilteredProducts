using PaginatedFilteredProducts.Domain.Common.Abstractions;
using PaginatedFilteredProducts.Domain.Products.ValueObjects;

namespace PaginatedFilteredProducts.Domain.Products.Aggregates;

public static class ReviewFactory
{
    public static Review CreateReview(string text, int rating)
    {
        var reviewId = Guid.NewGuid();
        var reviewText = new ReviewText(text);
        var reviewRating = new ReviewRating(rating);

        return new Review(reviewId, reviewText, reviewRating);
    }
}

public class Review : BaseEntity
{
    public new Guid Id { get; private set; }
    public ReviewText Text { get; private set; }
    public ReviewRating Rating { get; private set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } // Navigation Property

    private Review(){}
    
    public Review(Guid id, ReviewText text, ReviewRating rating)
    {
        Id = id;
        Text = text;
        Rating = rating;
    }
}