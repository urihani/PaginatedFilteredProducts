namespace PaginatedFilteredProducts.Application.Products.Dtos;

public class PaginatedProductsResultDto
{
    public IEnumerable<ProductDto> Products { get; set; }
    public int TotalCount { get; set; }
}