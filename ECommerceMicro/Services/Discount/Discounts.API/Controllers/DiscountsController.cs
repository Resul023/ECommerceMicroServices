using Discounts.API.Model;
using Discounts.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Services;
using System.Reflection;

namespace Discounts.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountsController : CustomControllerBases
{
    private readonly IDiscountService _discountService;

    private readonly ISharedIdentityService _sharedIdentityService;

    public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
    {
        this._discountService = discountService;
        this._sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResultInstance(await _discountService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var discount = await _discountService.GetById(id);

        return CreateActionResultInstance(discount);
    }

    [HttpGet]
    [Route("/api/[controller]/[action]/{code}")]
    public async Task<IActionResult> GetByCode(string code)

    {
        var userId = _sharedIdentityService.GetUserId;

        var discount = await _discountService.GetByCodeAndUserId(code, userId);
        return CreateActionResultInstance(discount);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Discount discount)
    {
        discount.UserId = _sharedIdentityService.GetUserId;
        return CreateActionResultInstance(await _discountService.Create(discount));
    }

    [HttpPut]
    public async Task<IActionResult> Update(Discount discount)
    {
        return CreateActionResultInstance(await _discountService.Update(discount));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return CreateActionResultInstance(await _discountService.Delete(id));
    }
}
