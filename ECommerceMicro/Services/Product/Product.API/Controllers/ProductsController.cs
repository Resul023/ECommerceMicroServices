using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Dtos.Product;
using Product.API.Services;
using Shared.ControllerBases;

namespace Product.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : CustomControllerBases
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        this._productService = productService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _productService.GetAllAsync();

        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _productService.GetByIdAsync(id);

        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
    {
        var response = await _productService.CreateAsync(productCreateDto);

        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        var response = await _productService.UpdateAsync(productUpdateDto);

        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _productService.DeleteAsync(id);

        return CreateActionResultInstance(response);
    }
}
