﻿using Product.API.Dtos.Product;
using Shared.Dtos;

namespace Product.API.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
        Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categotyUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
