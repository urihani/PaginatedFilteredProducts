namespace PaginatedFilteredProducts.Application.Products.Dtos;

public class ReviewDto
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public int Rating { get; set; }
}