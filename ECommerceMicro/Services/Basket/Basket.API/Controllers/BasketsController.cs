using Basket.API.Dtos;
using Basket.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Services;

namespace Basket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : CustomControllerBases
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
       this._basketService = basketService;
       this._sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
    {
        basketDto.UserId = _sharedIdentityService.GetUserId;
        var response = await _basketService.SaveOrUpdate(basketDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()

    {
        return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
    }
}
