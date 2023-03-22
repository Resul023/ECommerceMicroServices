using Product.API.Dtos.Product;
using Shared.Dtos;

namespace Product.API.Services;
public interface IProductService
{
    Task<Response<List<ProductDto>>> GetAllAsync();
    Task<Response<ProductDto>> GetByIdAsync(string id);
    Task<Response<ProductDto>> CreateAsync(ProductCreateDto courseCreateDto);
    Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);
    Task<Response<NoContent>> DeleteAsync(string id);
}
