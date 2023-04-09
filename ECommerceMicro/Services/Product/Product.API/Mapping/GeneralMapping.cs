using AutoMapper;
using Product.API.Dtos.Product;
namespace Product.API.Mapping;
public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();

        CreateMap<Models.Product, ProductDto>().ReverseMap();
        CreateMap<Models.Product, ProductCreateDto>().ReverseMap();
        CreateMap<Models.Product, ProductUpdateDto>().ReverseMap();
    }
}
