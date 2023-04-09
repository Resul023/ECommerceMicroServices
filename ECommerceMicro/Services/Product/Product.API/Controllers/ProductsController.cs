using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.API.Dtos.Product;
using Product.API.Services;
using Shared.ControllerBases;

namespace Product.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : CustomControllerBases
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        this._productService = productService;
        _logger = logger;
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
        _logger.LogInformation(response.ToString());
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
    {
        var response = await _productService.CreateAsync(productCreateDto);
        _logger.LogInformation(response.ToString());
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        var response = await _productService.UpdateAsync(productUpdateDto);
        _logger.LogInformation(response.ToString());
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _productService.DeleteAsync(id);
        _logger.LogInformation(response.ToString());
        return CreateActionResultInstance(response);
    }
}
