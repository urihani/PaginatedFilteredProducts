using AutoMapper;
using PaginatedFilteredProducts.Application.Products.Dtos;
using PaginatedFilteredProducts.Domain.Products.Aggregates;

namespace PaginatedFilteredProducts.Application.Products.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));

        // Using direct mapping for simplicity
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.Value)); 
    }
}