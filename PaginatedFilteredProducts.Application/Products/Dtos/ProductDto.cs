namespace PaginatedFilteredProducts.Application.Products.Dtos;

public class ProductDto
{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
}