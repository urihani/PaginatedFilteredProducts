using AutoMapper;
using PaginatedFilteredProducts.Application.Products.Dtos;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Application.Products.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value)) // Map from ProductName.Value to string
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value)) // Map from ProductDescription.Value to string
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency)) // Assuming Currency is a string property in the Money value object
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount)); // Assuming Amount is a decimal property in the Money value object

        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value)) // Map from ReviewText.Value to string
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.Value));
    }
}